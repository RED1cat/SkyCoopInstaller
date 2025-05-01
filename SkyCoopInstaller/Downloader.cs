using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace SkyCoopInstaller
{
    internal class Downloader
    {
        private static List<DownloadQueueElement> m_DownloadQueue;
        private static DownloadQueueElement m_CurrentlyDownloadingFile;
        private static bool m_EventSubscribbed;
        private static string m_Version;
        public static bool SilentMode = false;
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
            m_Version = release.m_EternalVersion;
            m_DownloadQueue = new List<DownloadQueueElement>();
            m_CurrentlyDownloadingFile = null;
            m_EventSubscribbed = false;
            m_DownloadQueue.Clear();
            if (!SilentMode)
            {
                Program.mainForm.ClearInstallLog();
            }
            if (Directory.Exists(gamePath + @"\Plugins"))
            {
                if (File.Exists(gamePath + @"\Plugins\AutoUpdatingPlugin.dll"))
                {
                    MessageBox.Show("Sky Co-Op " + release.m_ReleaseMeta.m_ReleaseName + " using specific versions of the dependency mods.\r\nAutoUpdatingPlugin will be deleted.",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.ServiceNotification);
                    File.Delete(gamePath + @"\Plugins\AutoUpdatingPlugin.dll");
                }
            }

            if (!SilentMode)
            {
                Program.mainForm.AddInstallLog("Checking currently installed Melonloader...");

                if (MelonChecker.CheckInstalledMelonVersion(gamePath, melonVersion) == false)
                {
                    Program.mainForm.AddInstallLog("Melonloader " + melonVersion + " going to be installed");
                    m_DownloadQueue.Add(new DownloadQueueElement(release.m_MelonURL, gamePath, "Melonloader", true));
                } else
                {
                    Program.mainForm.AddInstallLog("Melonloader " + melonVersion + " is already installed, skipping");
                    Program.mainForm.UpdateTotalProgressBar(1);
                }
            }
            else
            {
                Console.WriteLine("Melonloader " + melonVersion + " going to be installed");
                m_DownloadQueue.Add(new DownloadQueueElement(release.m_MelonURL, gamePath, "Melonloader", true));
            }

            if(!Directory.Exists(gamePath + @"\Mods"))
            {
                if (!SilentMode)
                {
                    Program.mainForm.AddInstallLog("Mods folder not exist, creating...");
                }
                else
                {
                    Console.WriteLine("Mods folder not exist, creating...");
                }
                Directory.CreateDirectory(gamePath + @"\Mods");
            }
            if (!Directory.Exists(gamePath + @"\Plugins"))
            {
                if (!SilentMode)
                {
                    Program.mainForm.AddInstallLog("Plugins folder not exist, creating...");
                }
                else
                {
                    Console.WriteLine("Plugins folder not exist, creating...");
                }
                Directory.CreateDirectory(gamePath + @"\Plugins");
            }

            m_DownloadQueue.Add(new DownloadQueueElement(release.m_ReleaseMeta.m_DownloadURL, gamePath + @"\Mods", "SkyCoop", true));

            if (!SilentMode)
            {
                Program.mainForm.AddInstallLog("Sky-Coop " + release.m_ReleaseMeta.m_ReleaseName + " from " + release.m_FromBranch + " branch going to be installed");
            }
            else
            {
                Console.WriteLine("Sky-Coop " + release.m_ReleaseMeta.m_ReleaseName + " from " + release.m_FromBranch + " branch going to be installed");
            }
            foreach (GithubManager.DependenceMeta dependenceMeta in release.m_Dependencies)
            {
                m_DownloadQueue.Add(new DownloadQueueElement(dependenceMeta.m_DownloadURL, gamePath + dependenceMeta.m_Path, dependenceMeta.m_Name, dependenceMeta.m_IsZip));
            }

            if (!SilentMode)
            {
                Program.mainForm.AddInstallLog("Found " + release.m_Dependencies.Count + " dependencies needed to be installed");
                Program.mainForm.SetMaximumValueProgressBar(release.m_Dependencies.Count + 2);
            }
            else
            {
                Console.WriteLine("Found " + release.m_Dependencies.Count + " dependencies needed to be installed");
            }
            BeginDownload();
        }

        private static void BeginDownload()
        {
            if(m_DownloadQueue.Count > 0) 
            {
                if (!m_EventSubscribbed)
                {
                    Program.webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    Program.webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                    m_EventSubscribbed = true;
                }
                DownloadNextFileFromQueue();
            }
        }

        private static void DownloadNextFileFromQueue()
        {
            if(m_DownloadQueue.Count > 0)
            {
                m_CurrentlyDownloadingFile = m_DownloadQueue[0];
                m_DownloadQueue.RemoveAt(0);
            } else
            {
                m_CurrentlyDownloadingFile = null;
                DownloadingQueueFinished();
                return;
            }
            
            
            Program.webClient.Headers.Clear();
            Program.webClient.Headers.Add("Cache-Control", "no-cache");
            Program.webClient.Headers.Add("User-Agent", "Unity web player");
            Program.webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            string FileName = m_CurrentlyDownloadingFile.m_FileName;

            if (!SilentMode)
            {
                Program.mainForm.AddInstallLog("Downloading " + FileName + "...");
            }
            else
            {
                Console.WriteLine("Downloading " + FileName + "...");
            }
            if (m_CurrentlyDownloadingFile.m_IsZip)
            {
                FileName += ".zip";
            } else
            {
                FileName += ".dll";
            }
            string PathPlusName = m_CurrentlyDownloadingFile.m_Path + @"\" + FileName;
            
            Program.webClient.DownloadFileAsync(new Uri(m_CurrentlyDownloadingFile.m_URL), PathPlusName);
        }
        private static void DownloadingQueueFinished()
        {
            Program.webClient.Dispose();
            Program.webClient = new WebClient();

            if (!SilentMode)
            {
                Program.mainForm.InstallFinished("Installation", m_Version);
            } else
            {
                Console.WriteLine($"Downloader finished version {m_Version}");
                AutoSetup.Installing = false;
            }
        }

        private static void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (!SilentMode)
            {
                Program.mainForm.UpdateCurrentFileProgressBar(e.ProgressPercentage, Downloader.m_CurrentlyDownloadingFile.m_FileName);
            }
            else
            {
                Console.Write('*');
            }
        }

        private static void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (!SilentMode)
            {
                if (e.Cancelled)
                {
                    MessageBox.Show("Downloading cancled",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.ServiceNotification);
                    return;
                }
                if (e.Error != null)
                {
                    MessageBox.Show($"Downloading error: {e.Error}",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.ServiceNotification);
                    return;
                }
            }
            else
            {
                if (e.Cancelled)
                {
                    Console.WriteLine("Downloading cancled");
                }
                if(e.Error != null)
                {
                    Console.WriteLine(e.Error.ToString());
                }
            }

            if (!SilentMode)
            {
                Program.mainForm.AddInstallLog(m_CurrentlyDownloadingFile.m_FileName + " downloaded!");
            }
            else
            {
                Console.WriteLine(m_CurrentlyDownloadingFile.m_FileName + " downloaded!");
            }
            if (m_CurrentlyDownloadingFile.m_IsZip)
            {
                if (!SilentMode)
                {
                    Program.mainForm.AddInstallLog("Unzipping " + m_CurrentlyDownloadingFile.m_FileName + "...");
                }
                else
                {
                    Console.WriteLine("Unzipping " + m_CurrentlyDownloadingFile.m_FileName + "...");
                }
                new ICSharpCode.SharpZipLib.Zip.FastZip().ExtractZip(m_CurrentlyDownloadingFile.m_Path + @"\" + m_CurrentlyDownloadingFile.m_FileName + ".zip", m_CurrentlyDownloadingFile.m_Path, null);

                File.Delete(m_CurrentlyDownloadingFile.m_Path + @"\" + m_CurrentlyDownloadingFile.m_FileName + ".zip");

                if (!SilentMode)
                {
                    Program.mainForm.UpdateTotalProgressBar(1);
                    Program.mainForm.AddInstallLog(m_CurrentlyDownloadingFile.m_FileName + " successfully extracted!");
                }
                else
                {
                    Console.WriteLine(m_CurrentlyDownloadingFile.m_FileName + " successfully extracted!");
                }
                DownloadNextFileFromQueue();
            } else
            {
                if (!SilentMode)
                {
                    Program.mainForm.UpdateTotalProgressBar(1);
                }
                DownloadNextFileFromQueue();
            }
        }
    }
}
