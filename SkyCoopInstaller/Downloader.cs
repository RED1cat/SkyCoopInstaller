using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace SkyCoopInstaller
{
    internal class Downloader
    {
        private static List<DownloadQueueElement> m_DownloadQueue;
        private static DownloadQueueElement m_CurrentlyDownloadingFile;
        private static bool m_EventSubscribbed;
        private static string m_ModVersion;
        public static bool SilentMode = false;      
        public static string m_CurrentGamePath = "";
        public class DownloadQueueElement
        {
            public string m_Source = "";
            public string m_nuGetFilePath = "";
            public string m_URL = "";
            public string m_Path = "";
            public string m_FileName = "";
            public bool m_IsZip = false;
            public bool m_IsSingleFile = false;

            public DownloadQueueElement(string url, string path, string filename, bool iszip, bool isSingleFile,
                string source, string nuGetFilePath = "") 
            { 
                m_URL = url;
                m_Path = path;
                m_FileName = filename;
                m_IsZip = iszip;
                m_IsSingleFile = isSingleFile;

                if (source == "Nuget")
                {
                    m_Source = source;
                    m_nuGetFilePath= nuGetFilePath;
                }
            }
        }

        public static async void Start(GithubManager.AvalibleRelease release, string gamePath, string melonVersion)
        {
            m_CurrentGamePath = gamePath;
            m_ModVersion = release.m_EternalVersion;
            m_DownloadQueue = new List<DownloadQueueElement>();
            m_CurrentlyDownloadingFile = null;
            m_EventSubscribbed = false;
            m_DownloadQueue.Clear();

            if (!Directory.Exists(FileManager.m_DownLoadTempPath))
            {
                if (System.OperatingSystem.IsWindows())
                {
                    Directory.CreateDirectory(FileManager.m_DownLoadTempPath);
                }
                else if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
                {
                    Directory.CreateDirectory(FileManager.m_DownLoadTempPath, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);
                }
            }

            if (!SilentMode)
            {
                Program.mainWindow.ClearInstallLog();
            }

            if (File.Exists(@$"{m_CurrentGamePath}/Mods/SkyCoop.dll")) //legacy check
            {
                Program.mainWindow.AddInstallLog("SkyCoop legacy detected, it will been moved to SkyCoopLegacy folder in game root folder...");
                await FileManager.CopyFilesRecursively(new DirectoryInfo(@$"{m_CurrentGamePath}/MelonLoader"), new DirectoryInfo(@$"{m_CurrentGamePath}/SkyCoopLegacy/MelonLoader"), true);
                await FileManager.CopyFilesRecursively(new DirectoryInfo(@$"{m_CurrentGamePath}/Mods"), new DirectoryInfo(@$"{m_CurrentGamePath}/SkyCoopLegacy/Mods"), true);
                await FileManager.CopyFilesRecursively(new DirectoryInfo(@$"{m_CurrentGamePath}/Plugins"), new DirectoryInfo(@$"{m_CurrentGamePath}/SkyCoopLegacy/Plugins"), true);
                await FileManager.CopyFilesRecursively(new DirectoryInfo(@$"{m_CurrentGamePath}/UserLibs"), new DirectoryInfo(@$"{m_CurrentGamePath}/SkyCoopLegacy/UserLibs"), true);
            }

            if(await FileManager.CreateDirectory(@$"{m_CurrentGamePath}/Plugins"))
            {
                if (!SilentMode)
                {
                    Program.mainWindow.AddInstallLog("Creating Plugins folder...");
                }
                else
                {
                    Console.WriteLine("Creating Plugins folder...");
                }
            }
            if (File.Exists(@$"{m_CurrentGamePath}/Plugins/AutoUpdatingPlugin.dll"))
            {
                await MessageBoxManager.GetMessageBoxStandard("Information",
                            $"Sky Co-Op {release.m_ReleaseMeta.m_ReleaseName} using specific versions of the dependency mods.\nAutoUpdatingPlugin will be deleted.",
                            ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Warning).ShowAsPopupAsync(Program.mainWindow);

                await FileManager.DeleteFile(@$"{m_CurrentGamePath}/Plugins/AutoUpdatingPlugin.dll");
            }

            if (await FileManager.CreateDirectory(@$"{m_CurrentGamePath}/Mods"))
            {
                if (!SilentMode)
                {
                    Program.mainWindow.AddInstallLog("Creating Mods folder...");
                }
                else
                {
                    Console.WriteLine("Creating Mods folder...");
                }
            }

            if (await FileManager.CreateDirectory(@$"{m_CurrentGamePath}/UserLibs"))
            {
                if (!SilentMode)
                {
                    Program.mainWindow.AddInstallLog("Creating UserLibs folder...");
                }
                else
                {
                    Console.WriteLine("Creating UserLibs folder...");
                }
            }

            if (!SilentMode)
            {
                Program.mainWindow.AddInstallLog("Checking currently installed Melonloader...");

                if (MelonChecker.CheckInstalledMelonVersion(m_CurrentGamePath, melonVersion) == false)
                {
                    Program.mainWindow.AddInstallLog($"Melonloader {melonVersion} going to be installed");
                    m_DownloadQueue.Add(new DownloadQueueElement(release.m_MelonURL, "", "Melonloader", true, false, "Github"));
                } 
                else
                {
                    Program.mainWindow.AddInstallLog($"Melonloader {melonVersion} is already installed, skipping");
                    Program.mainWindow.UpdateTotalProgressBar(1);
                }
            }
            else
            {
                Console.WriteLine($"Melonloader {melonVersion} going to be installed");
                m_DownloadQueue.Add(new DownloadQueueElement(release.m_MelonURL, "", "Melonloader", true, false, "Github"));
            }

            m_DownloadQueue.Add(new DownloadQueueElement(release.m_ReleaseMeta.m_DownloadURL, "Mods", "SkyCoop", true, false, "Github"));

            if (!SilentMode)
            {
                Program.mainWindow.AddInstallLog($"Sky-Coop {release.m_ReleaseMeta.m_ReleaseName} from {release.m_FromBranch} branch going to be installed");
            }
            else
            {
                Console.WriteLine($"Sky-Coop {release.m_ReleaseMeta.m_ReleaseName} from {release.m_FromBranch} branch going to be installed");
            }

            foreach (GithubManager.DependenceMeta dependenceMeta in release.m_Dependencies)
            {
                m_DownloadQueue.Add(new DownloadQueueElement(dependenceMeta.m_DownloadURL, dependenceMeta.m_Path, dependenceMeta.m_Name, dependenceMeta.m_IsZip, true, dependenceMeta.m_Source, dependenceMeta.m_nuGetFilePath));
            }

            if (!SilentMode)
            {
                Program.mainWindow.AddInstallLog($"Found {release.m_Dependencies.Count} dependencies needed to be installed");
                Program.mainWindow.SetMaximumValueProgressBar(release.m_Dependencies.Count + 2);
            }
            else
            {
                Console.WriteLine($"Found {release.m_Dependencies.Count} dependencies needed to be installed");
            }
            BeginDownload();
        }

        private static void BeginDownload()
        {
            if(m_DownloadQueue.Count > 0) 
            {
                if (!m_EventSubscribbed)
                {
                    Program.webClient.Dispose();
                    Program.webClient = new();
                    Program.webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    Program.webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                    m_EventSubscribbed = true;
                }
                DownloadNextFileFromQueue();
            }
        }

        private static async void DownloadNextFileFromQueue()
        {
            if(m_DownloadQueue.Count > 0)
            {
                m_CurrentlyDownloadingFile = m_DownloadQueue[0];
                m_DownloadQueue.RemoveAt(0);
            } 
            else
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
                Program.mainWindow.AddInstallLog($"Downloading {FileName} ...");
            }
            else
            {
                Console.WriteLine($"Downloading { FileName }...");
            }


            if (m_CurrentlyDownloadingFile.m_IsZip)
            {
                if(m_CurrentlyDownloadingFile.m_Source == "Nuget")
                {
                    FileName += ".nupkg";
                }
                else
                {
                    FileName += ".zip";
                }
            }
            else
            {
                FileName += ".dll";
            }
            string PathPlusName = $"{FileManager.m_DownLoadTempPath}/{FileName}";

            Program.webClient.DownloadFileAsync(new Uri(m_CurrentlyDownloadingFile.m_URL), PathPlusName);
            
        }
        private static void DownloadingQueueFinished()
        {
            Program.webClient.Dispose();
            Program.webClient = new WebClient();

            FileManager.DeletingDonwloadData();
            if (!SilentMode)
            {
                Program.mainWindow.InstallFinished("Installation", m_ModVersion);
            }
            else
            {
                Console.WriteLine($"Downloader finished version {m_ModVersion}");
                AutoSetup.Installing = false;
            }
        }

        private static void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (!SilentMode)
            {
                Program.mainWindow.UpdateCurrentFileProgressBar(e.ProgressPercentage, Downloader.m_CurrentlyDownloadingFile.m_FileName);
            }
            else
            {
                Console.Write('*');
            }
        }

        private static async void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (!SilentMode)
            {
                if (e.Cancelled)
                {
                    FileManager.DeletingDonwloadData();
                    await MessageBoxManager.GetMessageBoxStandard("Information",
                                $"Downloading cancled",
                                ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Info).ShowAsPopupAsync(Program.mainWindow);
                    return;
                }
                if (e.Error != null)
                {
                    FileManager.DeletingDonwloadData();
                    await MessageBoxManager.GetMessageBoxStandard("ERROR",
                                $"Downloading error:\n{e.Error}",
                                ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error).ShowAsPopupAsync(Program.mainWindow);
                    return;
                }
            }
            else
            {
                if (e.Cancelled)
                {
                    FileManager.DeletingDonwloadData();
                    Console.WriteLine("Downloading cancled");
                }
                if(e.Error != null)
                {
                    FileManager.DeletingDonwloadData();
                    Console.WriteLine(e.Error.ToString());
                }
            }

            if (!SilentMode)
            {
                Program.mainWindow.AddInstallLog($"{m_CurrentlyDownloadingFile.m_FileName} downloaded!");
            }
            else
            {
                Console.WriteLine($"{m_CurrentlyDownloadingFile.m_FileName} downloaded!");
            }
            if (m_CurrentlyDownloadingFile.m_IsZip)
            {
                if (!SilentMode)
                {
                    Program.mainWindow.AddInstallLog($"Unzipping {m_CurrentlyDownloadingFile.m_FileName}...");
                }
                else
                {
                    Console.WriteLine("Unzipping " + m_CurrentlyDownloadingFile.m_FileName + "...");
                }

                try 
                {
                    new ICSharpCode.SharpZipLib.Zip.FastZip().ExtractZip(@$"{FileManager.m_DownLoadTempPath}/{m_CurrentlyDownloadingFile.m_FileName}.{(m_CurrentlyDownloadingFile.m_Source == "Nuget" ? "nupkg" : "zip")}",
                        @$"{FileManager.m_DownLoadTempPath}/{m_CurrentlyDownloadingFile.m_FileName}", null);
                }
                catch(Exception ex)
                {
                    ((WebClient)sender).CancelAsync();
                    CancelDownload(ex.Message);
                    return;
                }

                if(m_CurrentlyDownloadingFile.m_IsSingleFile)
                {
                    if(!await FileManager.CopyFile(@$"{FileManager.m_DownLoadTempPath}/{m_CurrentlyDownloadingFile.m_FileName}/{(m_CurrentlyDownloadingFile.m_Source == "Nuget" ? $"{m_CurrentlyDownloadingFile.m_nuGetFilePath}/" : "")}{m_CurrentlyDownloadingFile.m_FileName}.dll",
                        @$"{m_CurrentGamePath}/{m_CurrentlyDownloadingFile.m_Path}/{m_CurrentlyDownloadingFile.m_FileName}.dll", true))
                    {
                        ((WebClient)sender).CancelAsync();
                        CancelDownload("");
                        return;
                    }
                }
                else
                {
                    if(!await FileManager.CopyFilesRecursively(new DirectoryInfo(@$"{FileManager.m_DownLoadTempPath}/{m_CurrentlyDownloadingFile.m_FileName}/"), 
                        new DirectoryInfo(@$"{m_CurrentGamePath}/{m_CurrentlyDownloadingFile.m_Path}/")))
                    {
                        ((WebClient)sender).CancelAsync();
                        CancelDownload("");
                        return;
                    }
                }

                if (!SilentMode)
                {
                    Program.mainWindow.UpdateTotalProgressBar(1);
                    Program.mainWindow.AddInstallLog($"{m_CurrentlyDownloadingFile.m_FileName} successfully extracted!");
                }
                else
                {
                    Console.WriteLine($"{m_CurrentlyDownloadingFile.m_FileName} successfully extracted!");
                }
                DownloadNextFileFromQueue();
            } 
            else
            {
                if (m_CurrentlyDownloadingFile.m_IsSingleFile)
                {

                    if(!await FileManager.CopyFile(@$"{FileManager.m_DownLoadTempPath}/{m_CurrentlyDownloadingFile.m_FileName}.dll",
                        @$"{m_CurrentGamePath}/{m_CurrentlyDownloadingFile.m_Path}/{m_CurrentlyDownloadingFile.m_FileName}.dll", true))
                    {
                        ((WebClient)sender).CancelAsync();
                        CancelDownload("");
                        return;
                    }
                }

                if (!SilentMode)
                {
                    Program.mainWindow.UpdateTotalProgressBar(1);
                }
                DownloadNextFileFromQueue();
            }
        }

        private static async void CancelDownload(string msg)
        {
            m_DownloadQueue.Clear();
            FileManager.DeletingDonwloadData();
            if (!SilentMode)
            {
                Program.mainWindow.AddInstallLog($"Download canceled!\n{msg}");
                await MessageBoxManager.GetMessageBoxStandard("Info",
                            $"Download canceled\n{msg}",
                            ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Info).ShowAsPopupAsync(Program.mainWindow);

                Program.mainWindow.DONE_Button.IsEnabled = true;
            }
        }
    }
}
