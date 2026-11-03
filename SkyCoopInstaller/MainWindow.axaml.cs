using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SkyCoopInstaller;

public partial class MainWindow : Window
{
    List<GithubManager.AvalibleRelease> lastSelectetAvalibleReleases;
    bool m_GameVersionSelected = false;
    bool m_ModReadyToInstall = false;
    bool m_ModInstalling = false;
    bool m_ModAlreadyInstalled = false;
    string m_CurrentInstalledVersion = string.Empty;

    public MainWindow(/*bool force = false*/) //TODO:need rework
    {
        InitializeComponent();
        if (true /*!force*/) //TODO:need rework
        {
            if (DateTime.Now.Month == 12 || DateTime.Now.Month < 3)
            {
                //SkyCoopLogo.Image = Properties.Resources.InstallerBanner2; //TODO:need rework
            }
            GithubManager.PrepareReleasesList();
            //NewsTextBox.Text = GithubManager.GetNews(); //TODO:need rework
            //CheckProgramVersion(); //TODO:need rework
            //MainTabControl.SelectedIndex = 0; //TODO:need rework
            //ProgramVersionLabel.Text = "Version: " + BuildInfo.Version; //TODO:need rework
        }
    }

    private async void Select_Button_OnClick(object? sender, RoutedEventArgs args)
    {
        var topLevel = TopLevel.GetTopLevel(this);

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "",
            AllowMultiple = false,
        });

        if (files.Count >= 1)
        {
            string gamePath = Directory.GetParent(files[0].Path.AbsolutePath).FullName;
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
                m_GameVersionSelected = false;
                Install_Button.IsEnabled = false;
            }
        }
    }

    private void Install_Button_OnClick(object? sender, RoutedEventArgs args)
    {
        if (m_ModReadyToInstall)
        {
            //InstallationLogTextBox.Text = "";
            //m_ModInstalling = true;
            //MainTabControl.SelectedIndex = 2;

            //GameVersion.Enabled = false;
            //ModVersions.Enabled = false;
            //SelectButton.Enabled = false;
            //NextButton.Enabled = false;
            //InstallButton.Enabled = false;
            //HidePreReleaseCheckBox.Enabled = false;
            //UnInstallButton.Enabled = false;

            GithubManager.AvalibleRelease release = lastSelectetAvalibleReleases[ModVersions_ComboBox.SelectedIndex];

            Downloader.Start(release, GamePath_TextBox.Text, release.m_RequiredMelon);
        }
    }

    private void FillGameVersionBox()
    {
        List<string> gameVersions = GithubManager.GetGameVersions();
        foreach (string version in gameVersions)
        {
            GameVersion_ComboBox.Items.Add(version);
        }
        GameVersion_ComboBox.SelectedItem = GameVersion_ComboBox.Items[0];
    }

    private async Task<bool> CheckingGameVersion(string gamePath)
    {
        string gameVersionFile = gamePath + @"\tld_Data\StreamingAssets\version.txt";
        bool thisVersionIsCompatible = false;
        string version = " ";
        string selectedCompatibleVersion = string.Empty;

        if (File.Exists(gameVersionFile))
        {
            version = File.ReadAllText(gameVersionFile).Split(' ')[0];
            if (GameVersion_ComboBox.Items.Count > 0)
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
        }

        if (thisVersionIsCompatible == false)
        {
            if (string.IsNullOrEmpty(version) || version == " ")
            {
                var msgBox = MessageBoxManager.GetMessageBoxStandard("ERROR", 
                    "File you selected does not looks like The Long Dark. Make sure you selected tld.exe file, AND NOT SHORTCUT! You need to select tld.exe from the directory of the game.", 
                    ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                var msgResult = await msgBox.ShowAsync();

                return false;
            }
            else
            {
                var msgBox = MessageBoxManager.GetMessageBoxStandard("Unknown version selected",
                    $"The version of the game you have chosen ({version}), does not match with any supported builds of the game. You can select version manually, and continue on your own risk.",
                    ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Warning);
                var msgResult = await msgBox.ShowAsync();
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
            //ChangeLogLabel.Text = lastSelectetAvalibleReleases[ModVersions.SelectedIndex].m_ReleaseMeta.m_ReleaseName;
            Install_Button.IsEnabled = true;
            m_ModReadyToInstall = true;
        }
    }

    private void CheckingForInstalledMod(string gamePath)
    {
        if (File.Exists(gamePath + @"\Mods\SkyCoop.dll") && Directory.Exists(gamePath + @"\MelonLoader"))
        {
            m_ModReadyToInstall = true;
            m_ModAlreadyInstalled = true;
            Uninstall_Button.IsEnabled = true;
            ChechingModVersion(gamePath);
        }
    }

    private void ChechingModVersion(string gamePath)
    {
        FileVersionInfo fileVersion = FileVersionInfo.GetVersionInfo(gamePath + @"\Mods\SkyCoop.dll");
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
}