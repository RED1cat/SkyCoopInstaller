using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Lifetime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SkyCoopInstaller
{
    internal class Downloader
    {
        private static List<DownloadQueueElement> DownloadQueue = new List<DownloadQueueElement>();
        public static DownloadQueueElement CurrentlyDownloadingFile = null;
        private static bool EventSubscribbed = false;
        public class DownloadQueueElement
        {
            public string m_URL = "";
            public string m_Path = "";
            public string m_FileName = "";
            public bool m_IsZip = false;

            public DownloadQueueElement(string url, string path, string filename, bool iszip) 
            { 
                m_URL = url;
                m_Path = path;
                m_FileName = filename;
                m_IsZip = iszip;
            }
        }

        public static void Start(GithubManager.AvalibleRelease release, string gamePath, string melonVersion)
        {
            DownloadQueue.Clear();
            Program.mainForm.ClearInstallLog();
            Program.mainForm.AddInstallLog("Checking currently installed Melonloader...");
            if (MelonChecker.CheckInstalledMelonVersion(gamePath, melonVersion) == false)
            {
                Program.mainForm.AddInstallLog("Melonloader "+ melonVersion + " going to be installed");
                DownloadQueue.Add(new DownloadQueueElement(release.m_MelonURL, gamePath, "Melonloader", true));
            }
            else
            {
                Program.mainForm.AddInstallLog("Melonloader " + melonVersion+" is already installed, skipping");
                Program.mainForm.UpdateTotalProgressBar(1);
            }

            if(!Directory.Exists(gamePath + @"\Mods"))
            {
                Program.mainForm.AddInstallLog("Mods folder not exist, creating...");
                Directory.CreateDirectory(gamePath + @"\Mods");
            }
            if (!Directory.Exists(gamePath + @"\Plugins"))
            {
                Program.mainForm.AddInstallLog("Plugins folder not exist, creating...");
                Directory.CreateDirectory(gamePath + @"\Plugins");
            }

            DownloadQueue.Add(new DownloadQueueElement(release.m_ReleaseMeta.m_DownloadURL, gamePath + @"\Mods", "SkyCoop", true));
            Program.mainForm.AddInstallLog("Sky-Coop " + release.m_ReleaseMeta.m_ReleaseName + " from "+ release.m_FromBranch+ " branch going to be installed");
            foreach (GithubManager.DependenceMeta dependenceMeta in release.m_Dependencies)
            {
                DownloadQueue.Add(new DownloadQueueElement(dependenceMeta.m_DownloadURL, gamePath + dependenceMeta.m_Path, dependenceMeta.m_Name, dependenceMeta.m_IsZip));
            }
            Program.mainForm.AddInstallLog("Found " + release.m_Dependencies.Count + " dependencies needed to be installed");
            BeginDownload();
        }

        private static void BeginDownload()
        {
            if(DownloadQueue.Count > 0) 
            {
                if (!EventSubscribbed)
                {
                    Program.webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    Program.webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                }
                DownloadNextFileFromQueue();
            }
        }

        private static void DownloadNextFileFromQueue()
        {
            if(DownloadQueue.Count > 0)
            {
                CurrentlyDownloadingFile = DownloadQueue[0];
                DownloadQueue.RemoveAt(0);
            } else
            {
                CurrentlyDownloadingFile = null;
                DownloadingQueueFinished();
                return;
            }
            
            
            Program.webClient.Headers.Clear();
            Program.webClient.Headers.Add("Cache-Control", "no-cache");
            Program.webClient.Headers.Add("User-Agent", "Unity web player");
            Program.webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            string FileName = CurrentlyDownloadingFile.m_FileName;
            Program.mainForm.AddInstallLog("Downloading " + FileName + "...");
            if (CurrentlyDownloadingFile.m_IsZip)
            {
                FileName += ".zip";
            } else
            {
                FileName += ".dll";
            }
            string PathPlusName = CurrentlyDownloadingFile.m_Path + @"\" + FileName;
            
            Program.webClient.DownloadFileAsync(new Uri(CurrentlyDownloadingFile.m_URL), PathPlusName);
        }
        private static void DownloadingQueueFinished()
        {
            Program.webClient.Dispose();
            Program.webClient = new WebClient();
            Program.mainForm.InstallFinished();
        }

        private static void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Program.mainForm.UpdateCurrentFileProgressBar(e.ProgressPercentage);
        }

        private static void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Downloading cancled");
                return;
            }
            if (e.Error != null)
            {
                MessageBox.Show("Downloading error");
                return;
            }

            Program.mainForm.AddInstallLog(CurrentlyDownloadingFile.m_FileName + " downloaded!");

            if (CurrentlyDownloadingFile.m_IsZip)
            {
                Program.mainForm.AddInstallLog("Unzipping "+ CurrentlyDownloadingFile.m_FileName + "...");
                new ICSharpCode.SharpZipLib.Zip.FastZip().ExtractZip(CurrentlyDownloadingFile.m_Path + @"\" + CurrentlyDownloadingFile.m_FileName + ".zip", CurrentlyDownloadingFile.m_Path, null);

                File.Delete(CurrentlyDownloadingFile.m_Path + @"\" + CurrentlyDownloadingFile.m_FileName + ".zip");
                Program.mainForm.UpdateTotalProgressBar(1);
                Program.mainForm.AddInstallLog(CurrentlyDownloadingFile.m_FileName+ " successfully extracted!");
                DownloadNextFileFromQueue();
            } else
            {
                Program.mainForm.UpdateTotalProgressBar(1);
                DownloadNextFileFromQueue();
            }
        }
    }
}
