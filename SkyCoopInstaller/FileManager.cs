using MsBox.Avalonia;
using MsBox.Avalonia.Base;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SkyCoopInstaller
{
    public static class FileManager
    {
        public static string m_News = "";
        public static string m_LatestInstallerVersion = "";
        public static string m_TempPath = @$"{Path.GetTempPath()}/SkyCoopInstaller";
        public static string m_DownLoadTempPath = @$"{Path.GetTempPath()}/SkyCoopInstaller/Downloads";
        private static List<DownloadData> m_InstallerDatas = new List<DownloadData>();
        private static IMsBox<string> FetchInstallerDataMsgBox = MessageBoxManager.GetMessageBoxCustom(new MsBox.Avalonia.Dto.MessageBoxCustomParams
        {
            ButtonDefinitions = new List<ButtonDefinition>(),
            ContentTitle = "INFO",
            ContentMessage = "Downloading installer data",
            Icon = Icon.Info,
            WindowStartupLocation = Avalonia.Controls.WindowStartupLocation.CenterOwner,
            SizeToContent = Avalonia.Controls.SizeToContent.WidthAndHeight,
            CanResize = false,
            ShowInCenter = true
        });
        public struct DownloadData
        {
            public Uri m_Uri;
            public string m_FileName;
            public DownloadData(Uri uri, string fileName)
            {
                m_Uri = uri;
                m_FileName = fileName;
            }
        }

        public static async Task FetchInstallerData(bool force = false)
        {
            if (!Directory.Exists(m_TempPath))
            {
                if (!await CreateDirectory(m_TempPath))
                    Environment.Exit(0);
                force = true;
            }
            else if (!File.Exists(@$"{m_TempPath}/Version.txt") || !File.Exists(@$"{m_TempPath}/NEWS.txt") || !File.Exists(@$"{m_TempPath}/Releases.json") || !File.Exists(@$"{m_TempPath}/FilteredReleases.json"))
            {
                force = true;
            }
            Program.webClient.Dispose();
            Program.webClient = new();
            Program.webClient.Headers.Clear();
            Program.webClient.Headers.Add("Cache-Control", "no-cache");
            Program.webClient.Headers.Add("User-Agent", "Unity web player");
            Program.webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);
            Program.webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(WebClient_FetchInstallerDataCompleted);

            m_InstallerDatas.Clear();
            m_InstallerDatas.Add(new DownloadData(new Uri(GithubManager.LatestInstllaerVersionURL), @$"{m_TempPath}/Version.txt"));
            m_InstallerDatas.Add(new DownloadData(new Uri(GithubManager.NewsURL), @$"{m_TempPath}/NEWS.txt"));
            if (force)
            {
                m_InstallerDatas.Add(new DownloadData(new Uri(GithubManager.MetadataURL), @$"{m_TempPath}/Releases.json"));
                m_InstallerDatas.Add(new DownloadData(new Uri(GithubManager.FilteredURL), @$"{m_TempPath}/FilteredReleases.json"));
            }
            WebClient_FetchInstallerDataCompleted(Program.webClient, new AsyncCompletedEventArgs(null, false, null));
            await FetchInstallerDataMsgBox.ShowAsPopupAsync(Program.mainWindow);

            m_News = await ReadFileData(@$"{m_TempPath}/NEWS.txt");
            m_LatestInstallerVersion = await ReadFileData(@$"{m_TempPath}/Version.txt");
            m_LatestInstallerVersion = m_LatestInstallerVersion.TrimEnd('\n');
        }

        private static async void WebClient_FetchInstallerDataCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (m_InstallerDatas.Count > 0)
            {
                try
                {
                    Program.webClient.DownloadFileAsync(m_InstallerDatas[m_InstallerDatas.Count - 1].m_Uri, m_InstallerDatas[m_InstallerDatas.Count - 1].m_FileName);
                    m_InstallerDatas.RemoveAt(m_InstallerDatas.Count - 1);
                }
                catch (Exception ex)
                {
                    m_InstallerDatas.Clear();
                    ((WebClient)sender).CancelAsync();
                    ((WebClient)sender).Dispose();

                    FetchInstallerDataMsgBox.Close();
                    ButtonResult msgBoxResult = await MessageBoxManager.GetMessageBoxStandard("ERROR",
                                $"Error while fetching installer data:\n{ex.Message}",
                                ButtonEnum.Ok, Icon.Error).ShowAsPopupAsync(Program.mainWindow);
                    if (msgBoxResult == ButtonResult.Ok)
                    {
                        Environment.Exit(0);
                    }
                }
            }
            else
            {
                FetchInstallerDataMsgBox.Close();
            }
        }

        public static async Task<string> ReadFileData(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    return File.ReadAllText(filePath);
                }
                catch (Exception e)
                {
                    await MessageBoxManager.GetMessageBoxStandard("ERROR",
                                $"Error while reading file: {filePath}\n{e.Message}",
                                ButtonEnum.Ok, Icon.Error).ShowAsPopupAsync(Program.mainWindow);
                    return string.Empty;
                }
            }
            else
            {
                await MessageBoxManager.GetMessageBoxStandard("ERROR",
                            $"File({filePath}) didn't exist",
                            ButtonEnum.Ok, Icon.Error).ShowAsPopupAsync(Program.mainWindow);
                return string.Empty;
            }
        }

        public static async Task<bool> CreateDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                try
                {
                    if (System.OperatingSystem.IsWindows())
                    {
                        Directory.CreateDirectory(directory);
                    }
                    else if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
                    {
                        Directory.CreateDirectory(directory, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);
                    }
                }
                catch (Exception e)
                {
                    await MessageBoxManager.GetMessageBoxStandard("ERROR",
                            $"Error while creating directory: {directory}\n{e.Message}",
                            ButtonEnum.Ok, Icon.Error).ShowAsPopupAsync(Program.mainWindow);
                    return false;
                }
            }
            return true;
        }

        public static async Task<bool> DeleteFile(string file)
        {
            if (File.Exists(file))
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception e)
                {
                    await MessageBoxManager.GetMessageBoxStandard("ERROR",
                            $"Error while deleting file: {file}\n{e.Message}",
                            ButtonEnum.Ok, Icon.Error).ShowAsPopupAsync(Program.mainWindow);
                    return false;
                }
            }
            return true;
        }

        public static async Task<bool> CopyFile(string sourceFile, string targetFile, bool removeSource = false)
        {
            if (File.Exists(sourceFile))
            {
                try
                {
                    File.Copy(sourceFile, targetFile, true);
                }
                catch (Exception e)
                {
                    await MessageBoxManager.GetMessageBoxStandard("ERROR",
                            $"Error while copying file: {sourceFile}\n{e.Message}",
                            ButtonEnum.Ok, Icon.Error).ShowAsPopupAsync(Program.mainWindow);
                    return false;
                }
            }
            return true;
        }

        public static async Task<bool> CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target, bool removeSource = false)
        {
            if (source.Exists)
            {
                try
                {
                    if (!target.Exists)
                        if (!await CreateDirectory(target.FullName))
                            return false;
                    foreach (DirectoryInfo dir in source.GetDirectories())
                        await CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
                    foreach (FileInfo file in source.GetFiles())
                        file.CopyTo(Path.Combine(target.FullName, file.Name), true);
                    if (removeSource)
                        if (source.Exists)
                            source.Delete(true);
                }
                catch (Exception e)
                {
                    await MessageBoxManager.GetMessageBoxStandard("ERROR",
                            $"Error while copying file: {source}\n{e.Message}",
                            ButtonEnum.Ok, Icon.Error).ShowAsPopupAsync(Program.mainWindow);
                    return false;
                }
            }
             return true;
        }

        public static async void DeletingDonwloadData()
        {
            if (Directory.Exists(m_DownLoadTempPath))
            {
                try
                {
                    Directory.Delete(m_DownLoadTempPath, true);
                }
                catch(Exception e)
                {
                    await MessageBoxManager.GetMessageBoxStandard("ERROR",
                                $"Error while deleting download folder:\n{e.Message}",
                                ButtonEnum.Ok, Icon.Error).ShowAsPopupAsync(Program.mainWindow);
                }
            }
        }
    }
}
