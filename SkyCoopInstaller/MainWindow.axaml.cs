using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Platform.Storage;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SkyCoopInstaller;

public partial class MainWindow : Window
{
    List<GithubManager.AvalibleRelease> lastSelectetAvalibleReleases;
    bool m_ModReadyToInstall = false;
    bool m_ModAlreadyInstalled = false;
    string m_CurrentInstalledVersion = string.Empty;

    public MainWindow(/*bool force = false*/) //TODO:need rework
    {
        InitializeComponent();

        Program.mainWindow = this;
        if (true /*!force*/) //TODO:need rework
        {
            if (DateTime.Now.Month == 12 || DateTime.Now.Month < 3)
            {
                SkyCoopLogo_Image.Source = new Bitmap(AssetLoader.Open(new Uri("avares://SkyCoopInstaller/Resources/InstallerBanner2.png")));
            }
#if !DEBUG
            Starting();
#endif
        }
    }

    public async void Starting(bool forceUpdate = false)
    {
        Main_TabControl.SelectedIndex = 0;
        ProgramVersion_Label.Content = $"Version: {BuildInfo.Version}";

        await FileManager.FetchInstallerData(forceUpdate);
        await Updater.CheckForUpdate();
        GithubManager.PrepareReleasesList();
        News_TextBox.Text = FileManager.m_News;
    }

    private void Main_TabControl_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (Main_TabControl != null)
        {
            switch (Main_TabControl.SelectedIndex)
            {
                case 0:
                    News_TextBox.IsVisible = true;
                    ChangeLog_Label.IsVisible = false;
                    ChangeLog_TextBox.IsVisible = false;
                    InstallationLog_TextBox.IsVisible = false;
                    break;
                case 1:
                    if (m_ModAlreadyInstalled)
                    {
                        Install_Button.Content = "UPDATE";
                        Uninstall_Button.IsEnabled = true;
                    }
                    else
                    {
                        Install_Button.Content = "INSTALL";
                        Uninstall_Button.IsEnabled = false;
                    }
                    News_TextBox.IsVisible = false;
                    ChangeLog_Label.IsVisible = true;
                    ChangeLog_TextBox.IsVisible = true;
                    InstallationLog_TextBox.IsVisible = false;
                    break;
                case 2:
                    DONE_Button.IsEnabled = false;
                    News_TextBox.IsVisible = false;
                    ChangeLog_Label.IsVisible = false;
                    ChangeLog_TextBox.IsVisible = false;
                    InstallationLog_TextBox.IsVisible = true;
                    break;
            }
        }
    }

    private async void Select_Button_Click(object? sender, RoutedEventArgs args)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel != null)
        {
            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                FileTypeFilter = [new FilePickerFileType("tld") {Patterns = ["*.exe", "*.x86_64"] }, FilePickerFileTypes.All],
                Title = "",
                AllowMultiple = false,
            });
            if (files.Count >= 1)
            {
                string gamePath = Directory.GetParent(files[0].Path.LocalPath).FullName;
                GamePath_TextBox.Text = gamePath;

                GameVersion_ComboBox.IsEnabled = true;

                FillGameVersionBox();

                if (await CheckingGameVersion(gamePath))
                {
                    UpdateReleaseList();
                    CheckingForInstalledMod(gamePath);
                }
                else
                {
                    m_ModReadyToInstall = false;
                    Install_Button.IsEnabled = false;
                }
            }
        }
    }
    private void GameVersion_ComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (GameVersion_ComboBox.SelectedItem == null)
        {
            return;
        }
        Next_Button.IsEnabled = true;
    }
    private void Next_Button_Click(object? sender, RoutedEventArgs e)
    {
        UpdateReleaseList();
        Main_TabControl.SelectedIndex = 1;
    }
    private void HidePreRelease_CheckBox_Click(object? sender, RoutedEventArgs e)
    {
        UpdateReleaseList();
    }
    private void Install_Button_Click(object? sender, RoutedEventArgs args)
    {
        if (m_ModReadyToInstall)
        {
            InstallationLog_TextBox.Text = "";
            Main_TabControl.SelectedIndex = 2;

            GithubManager.AvalibleRelease release = lastSelectetAvalibleReleases[ModVersions_ComboBox.SelectedIndex];

            Downloader.Start(release, GamePath_TextBox.Text, release.m_RequiredMelon);
        }
    }
    private async void Uninstall_Button_Click(object? sender, RoutedEventArgs e)
    {
        string msgResult = await MessageBoxManager.GetMessageBoxCustom(
            new MsBox.Avalonia.Dto.MessageBoxCustomParams
            {
                ButtonDefinitions = new List<ButtonDefinition>
                {
                    new ButtonDefinition {Name = "All", IsDefault = true},
                    new ButtonDefinition {Name = "Only SkyCoop", IsDefault = false},
                    new ButtonDefinition {Name = "Cancel", IsCancel = true}
                },
                ContentTitle = "UNINSTALLING",
                ContentMessage = "Choose uninstalling options",
                Icon = MsBox.Avalonia.Enums.Icon.Question,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                CanResize = false,
                SizeToContent = SizeToContent.WidthAndHeight,
                ShowInCenter = true,
                Topmost = false
            }
        ).ShowAsPopupAsync(this);

        if (msgResult != "Cancel")
        {
            InstallationLog_TextBox.Text = "";
            Main_TabControl.SelectedIndex = 2;
            Uninstaller.Start(GithubManager.GetReleasesByEternalVersion(GameVersion_ComboBox.SelectedItem.ToString(), m_CurrentInstalledVersion), GamePath_TextBox.Text, msgResult == "All");
            m_ModAlreadyInstalled = false;
        }
    }

    private void Back_Button_Click(object? sender, RoutedEventArgs e)
    {
        Main_TabControl.SelectedIndex = 0;
    }

    private void DONE_Button_Click(object? sender, RoutedEventArgs e)
    {
        Main_TabControl.SelectedIndex = 1;
    }

    private void UpdateInstallerData_Button_Click(object? sender, RoutedEventArgs e)
    {
        Starting(true);
    }

    private void FillGameVersionBox()
    {
        List<string> gameVersions = GithubManager.GameVersions;
        foreach (string version in gameVersions)
        {
            GameVersion_ComboBox.Items.Add(version);
        }
        if (GameVersion_ComboBox.ItemCount >= 1)
        {
            GameVersion_ComboBox.SelectedItem = GameVersion_ComboBox.Items[0];
        }
    }

    private async Task<bool> CheckingGameVersion(string gamePath)
    {
        string gameVersionFile = @$"{gamePath}/tld_Data/StreamingAssets/version.txt";
        bool thisVersionIsCompatible = false;
        string version = " ";
        string selectedCompatibleVersion = string.Empty;


        version = (await FileManager.ReadFileData(gameVersionFile)).Split(' ')[0];
        if (GameVersion_ComboBox.Items.Count > 0 && version != string.Empty)
        {
            foreach (string selectedVersion in GameVersion_ComboBox.Items)
            {
                if (selectedVersion.Contains("-"))
                {
                    string[] versionRange = selectedVersion.Split('-');

                    int one = int.Parse(version.Split('.')[1]);
                    int tho = int.Parse(versionRange[0].Split('.')[1]);
                    int thre = int.Parse(versionRange[1].Split('.')[1]);
                    if (one >= tho && one <= thre)
                    {
                        thisVersionIsCompatible = true;
                        selectedCompatibleVersion = selectedVersion;
                    }
                }
                else if (selectedVersion == version)
                {
                    thisVersionIsCompatible = true;
                    selectedCompatibleVersion = selectedVersion;
                }
            }
            if (selectedCompatibleVersion != string.Empty)
            {
                GameVersion_ComboBox.SelectedItem = selectedCompatibleVersion;
            }
        }

        if (thisVersionIsCompatible == false)
        {
            if (string.IsNullOrEmpty(version) || version == " ")
            {
                await MessageBoxManager.GetMessageBoxStandard("ERROR",
                    "File you selected does not looks like The Long Dark. Make sure you selected tld.exe file, AND NOT SHORTCUT! You need to select tld.exe from the directory of the game.",
                    ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error).ShowAsPopupAsync(this);

                return false;
            }
            else
            {
                await MessageBoxManager.GetMessageBoxStandard("Unknown version selected",
                    $"The version of the game you have chosen ({version}), does not match with any supported builds of the game. You can select version manually, and continue on your own risk.",
                    ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Warning).ShowAsPopupAsync(this);
            }
        }

        return true;
    }

    private void UpdateReleaseList()
    {
        List<GithubManager.AvalibleRelease> avalibleReleases = GithubManager.GetReleasesForGameVersion(GameVersion_ComboBox.SelectedItem.ToString(), (bool)HidePreRelease_CheckBox.IsChecked);
        if (avalibleReleases != null)
        {
            ModVersions_ComboBox.Items.Clear();
            lastSelectetAvalibleReleases = avalibleReleases;
            foreach (GithubManager.AvalibleRelease release in avalibleReleases)
            {
                ModVersions_ComboBox.Items.Add(release.m_ReleaseMeta.m_ReleaseName);
            }
            ModVersions_ComboBox.SelectedItem = ModVersions_ComboBox.Items[0];
        }
        UpdateReleaseInfo(this, EventArgs.Empty);
    }
    private void UpdateReleaseInfo(object sender, EventArgs e)
    {
        if (lastSelectetAvalibleReleases != null)
        {
            ChangeLog_TextBox.Text = lastSelectetAvalibleReleases[ModVersions_ComboBox.SelectedIndex].m_ReleaseMeta.m_ChangeLog;
            ChangeLog_Label.Content = lastSelectetAvalibleReleases[ModVersions_ComboBox.SelectedIndex].m_ReleaseMeta.m_ReleaseName;
            Install_Button.IsEnabled = true;
            m_ModReadyToInstall = true;
        }
    }

    private void CheckingForInstalledMod(string gamePath)
    {
        if ((File.Exists(@$"{gamePath}/Mods/SkyCoop.dll") || File.Exists($@"{gamePath}/Mods/SkyCoopClient.dll")) && Directory.Exists($@"{gamePath}/MelonLoader"))
        {
            m_ModReadyToInstall = true;
            m_ModAlreadyInstalled = true;
            Uninstall_Button.IsEnabled = true;
            ChechingModVersion(gamePath);
        }
    }

    private void ChechingModVersion(string gamePath)
    {
        FileVersionInfo fileVersion;
        if (File.Exists($@"{gamePath}/Mods/SkyCoop.dll"))
        {
            fileVersion = FileVersionInfo.GetVersionInfo($@"{gamePath}/Mods/SkyCoop.dll");
        }
        else if (File.Exists($@"{gamePath}/Mods/SkyCoopClient.dll"))
        {
            fileVersion = FileVersionInfo.GetVersionInfo($@"{gamePath}/Mods/SkyCoopClient.dll");
        }
        else
        {
            return;
        }

        GithubManager.AvalibleRelease releaes = GithubManager.GetReleasesByEternalVersion(GameVersion_ComboBox.SelectedItem.ToString(), fileVersion.FileVersion);
        if (releaes != null)
        {
            HidePreRelease_CheckBox.IsChecked = !releaes.m_IsPreRelease;
            ModVersions_ComboBox.SelectedItem = releaes.m_ReleaseMeta.m_ReleaseName;
            m_CurrentInstalledVersion = releaes.m_EternalVersion;
        }
        else
        {
            GithubManager.AvalibleRelease lastRelease = GithubManager.GetLatestReleaseMeta(GameVersion_ComboBox.SelectedItem.ToString(), true);
            if (lastRelease != null)
            {
                HidePreRelease_CheckBox.IsChecked = !lastRelease.m_IsPreRelease;
                ModVersions_ComboBox.SelectedItem = lastRelease.m_ReleaseMeta.m_ReleaseName;
                m_CurrentInstalledVersion = lastRelease.m_EternalVersion;
            }
        }
    }

    public void SetMaximumValueProgressBar(int value)
    {
        TotalProgress_ProgressBar.Maximum = value;
    }
    public void UpdateTotalProgressBar(int value)
    {
        TotalProgress_ProgressBar.Value += value;
        TotalProgress_label.Content = $"Total Progress: {TotalProgress_ProgressBar.Value}/{TotalProgress_ProgressBar.Maximum} Files";
    }
    public void UpdateCurrentFileProgressBar(int value, string text)
    {
        Progress_ProgressBar.Value = value;
        Progress_label.Content = $"{text} {value}%";
    }
    public void AddInstallLog(string Message)
    {
        if (string.IsNullOrEmpty(InstallationLog_TextBox.Text))
        {
            InstallationLog_TextBox.Text = $"[{DateTime.Now:T}] {Message}";
        }
        else
        {
            InstallationLog_TextBox.Text += $"\n[{DateTime.Now:T}] {Message}";
            InstallationLog_TextBox.ScrollToLine(InstallationLog_TextBox.GetLineCount() - 1);
        }
    }
    public void ClearInstallLog()
    {
        InstallationLog_TextBox.Text = string.Empty;
    }

    public async void InstallFinished(string message, string version = "")
    {
        TotalProgress_label.Content = "Total Progress: Done";
        Progress_label.Content = $"{message} completed";
        AddInstallLog($"{message} completed!");
        TotalProgress_ProgressBar.Value = 0;
        Progress_ProgressBar.Value = 0;
        TotalProgress_label.Content = "Total Progress:";
        Progress_label.Content = "Progress:";
        m_ModAlreadyInstalled = true;
        DONE_Button.IsEnabled = true;

        if (message == "Installation")
        {
            m_CurrentInstalledVersion = version;

            if (OperatingSystem.IsWindows())
            {
                if (File.Exists(@$"{Downloader.m_CurrentGamePath}/version.dll"))
                {
                    try
                    {
                        File.Move(@$"{Downloader.m_CurrentGamePath}/version.dll", @$"{Downloader.m_CurrentGamePath}/winhttp.dll", true);
                    }
                    catch (Exception e)
                    {
                        await MessageBoxManager.GetMessageBoxStandard("ERROR",
                                    e.Message,
                                    ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error).ShowAsPopupAsync(this);
                    }
                }
            }
            else if (OperatingSystem.IsLinux())
            {
                await MessageBoxManager.GetMessageBoxStandard("INFO",
                            $"To launch mod set the game's Steam Launch Options to:\nLD_LIBRARY_PATH=\"{Downloader.m_CurrentGamePath}:$LD_LIBRARY_PATH\" LD_PRELOAD=\"MelonLoader.Bootstrap.so:$LD_PRELOAD\" %command%",
                            ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Info).ShowAsPopupAsync(this);
            }
            else if (OperatingSystem.IsMacOS())
            {
                await MessageBoxManager.GetMessageBoxStandard("INFO",
                            $"To launch mod set the game's Steam Launch Options to:\n\"{Downloader.m_CurrentGamePath}/melonloader-launch.sh\" %command%",
                            ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Info).ShowAsPopupAsync(this);
            }
        }
        else
        {
            m_CurrentInstalledVersion = string.Empty;
        }

        await MessageBoxManager.GetMessageBoxStandard("Information",
            $"{message} completed!",
            ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Info).ShowAsPopupAsync(this);
    }
}