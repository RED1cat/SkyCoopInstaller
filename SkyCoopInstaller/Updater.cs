using MsBox.Avalonia;
using MsBox.Avalonia.Base;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SkyCoopInstaller
{
    public static class Updater
    {
        private static IMsBox<string> DonwloadInstallerUpdateMsgBox = MessageBoxManager.GetMessageBoxCustom(new MsBox.Avalonia.Dto.MessageBoxCustomParams
        {
            ButtonDefinitions = new List<ButtonDefinition>(),
            ContentTitle = "INFO",
            ContentMessage = "Updating installer",
            Icon = Icon.Info,
            WindowStartupLocation = Avalonia.Controls.WindowStartupLocation.CenterOwner,
            SizeToContent = Avalonia.Controls.SizeToContent.WidthAndHeight,
            CanResize = false,
            ShowInCenter = true
        });
        private static bool m_DownloadInProgress = false;
        public static async Task CheckForUpdate()
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.GetLength(0) > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].Contains("--instcopy"))
                    {
                        string[] sargs = args[i].Split('|');
                        if(sargs.Length == 3 && int.TryParse(sargs[1], out int cid))
                            Updater.CopyUpdate(cid, sargs[2]);
                        return;
                    }
                    else if(args[i].Contains("--instdel"))
                    {
                        string[] sargs = args[i].Split('|');
                        if (sargs.Length == 3 && int.TryParse(sargs[1], out int did))
                            Updater.DelUpdate(did, sargs[2]);
                        return;
                    }
                }
            }

            if (FileManager.m_LatestInstallerVersion.Replace("\n", "") != BuildInfo.Version)
            {
                m_DownloadInProgress = false;
                Program.webClient.Dispose();
                Program.webClient = new();
                Program.webClient.Headers.Clear();
                Program.webClient.Headers.Add("Cache-Control", "no-cache");
                Program.webClient.Headers.Add("User-Agent", "Unity web player");
                Program.webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);
                Program.webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;

                WebClient_DownloadFileCompleted(Program.webClient, new AsyncCompletedEventArgs(null, false, null));
                await DonwloadInstallerUpdateMsgBox.ShowAsPopupAsync(Program.mainWindow);

                if (Environment.ProcessPath != null)
                {
                    if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
                        File.SetUnixFileMode(@$"{FileManager.m_TempPath}/SkyCoopInstaller.{(OperatingSystem.IsLinux() ? "linux" : OperatingSystem.IsMacOS() ? "app" : "")}",
                            UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);

                        Process.Start(new ProcessStartInfo(@$"{FileManager.m_TempPath}/SkyCoopInstaller.{(OperatingSystem.IsWindows() ? "exe" : OperatingSystem.IsLinux() ? "linux" : OperatingSystem.IsMacOS() ? "app" : "")}",
                        $"--instcopy|{Environment.ProcessId}|{Environment.ProcessPath}"));
                    Environment.Exit(1);
                }
            }
        }

        private static async void WebClient_DownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            if (!m_DownloadInProgress)
            {
                try
                {
                    m_DownloadInProgress = true;
                    Program.webClient.DownloadFileAsync(new Uri($"{GithubManager.LatestInstllaerExe}.{(OperatingSystem.IsWindows() ? "exe" : OperatingSystem.IsLinux() ? "linux" : OperatingSystem.IsMacOS() ? "app" : "")}"), 
                        @$"{FileManager.m_TempPath}/SkyCoopInstaller.{(OperatingSystem.IsWindows() ? "exe" : OperatingSystem.IsLinux() ? "linux" : OperatingSystem.IsMacOS() ? "app" : "")}");
                }
                catch (Exception ex)
                {
                    ((WebClient)sender).CancelAsync();
                    ((WebClient)sender).Dispose();

                    DonwloadInstallerUpdateMsgBox.Close();
                    await MessageBoxManager.GetMessageBoxStandard("ERROR",
                                $"Error while download installer update:\n{ex.Message}",
                                ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error).ShowAsPopupAsync(Program.mainWindow);
                    Environment.Exit(1);
                }
            }
            else
            {
                DonwloadInstallerUpdateMsgBox.Close();
            }
        }

        public static async void CopyUpdate(int processId, string path)
        {
            try
            {
                var prevProc = TryGetProcessById(processId);
                if (prevProc != null)
                    await prevProc.WaitForExitAsync();
            }
            catch (Exception e)
            {
                await MessageBoxManager.GetMessageBoxStandard("ERROR",
                        e.Message,
                        ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error).ShowAsPopupAsync(Program.mainWindow);
                Environment.Exit(1);
            }

            if (Environment.ProcessPath != null)
            {
                await FileManager.CopyFile(Environment.ProcessPath, path);
                if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
                    File.SetUnixFileMode(path,
                        UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);

                Process.Start(new ProcessStartInfo(path,
                    $"--instdel|{Environment.ProcessId}|{Environment.ProcessPath}"));
                Environment.Exit(1);
            }
        }

        public static async void DelUpdate(int processId, string path)
        {
            try
            {
                var prevProc = TryGetProcessById(processId);
                if (prevProc != null)
                    await prevProc.WaitForExitAsync();
            }
            catch (Exception e)
            {
                await MessageBoxManager.GetMessageBoxStandard("ERROR",
                        e.Message,
                        ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error).ShowAsPopupAsync(Program.mainWindow);
                Environment.Exit(1);
            }

            if (Environment.ProcessPath != null)
                await FileManager.DeleteFile(path);
        }

        private static Process? TryGetProcessById(int pid)
        {
            try
            {
                var proc = Process.GetProcessById(pid);
                return proc.HasExited ? null : proc;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
