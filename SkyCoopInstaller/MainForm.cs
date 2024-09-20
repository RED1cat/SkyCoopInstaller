using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SkyCoopInstaller
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        List<GithubManager.AvalibleRelease> lastSelectetAvalibleReleases;
        bool m_GameVersionSelected = false;
        bool m_ModReadyToInstall = false;
        bool m_ModInstalling = false;
        bool m_ModAlreadyInstalled = false;
        string m_CurrentInstalledVersion = string.Empty;

        public MainForm()
        {
            if (AutoSetup.IsRequired())
            {
                AutoSetup.Begin();
                return;
            }
            InitializeComponent();
            if (DateTime.Now.Month == 12 || DateTime.Now.Month < 3)
            {
                SkyCoopLogo.Image = Properties.Resources.InstallerBanner2;
            }
            GithubManager.PrepareReleasesList();
            NewsTextBox.Text = GithubManager.GetNews();
            CheckProgramVersion();
            MainTabControl.SelectedIndex = 0;
            ProgramVersionLabel.Text = "Version: " + BuildInfo.Version;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "tld.exe (*.exe)|*.exe";
                ofd.RestoreDirectory = true;
                ofd.Multiselect = false;
                ofd.DereferenceLinks = false;
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    string gamePath = Directory.GetParent(ofd.FileName).FullName;
                    GamePath.Text = gamePath;

                    GameVersion.Enabled = true;

                    FillGameVersionBox();

                    if (CheckingGameVersion(gamePath))
                    {
                        UpdateReleaseList();
                        CheckingForInstalledMod(gamePath);
                    } else
                    {
                        m_ModReadyToInstall = false;
                        m_GameVersionSelected = false;
                        InstallButton.Enabled = false;
                    }
                }
            }
        }
        private void ChechingModVersion(string gamePath)
        {
            FileVersionInfo fileVersion = FileVersionInfo.GetVersionInfo(gamePath + @"\Mods\SkyCoop.dll");
            GithubManager.AvalibleRelease releaes = GithubManager.GetReleasesByEternalVersion(GameVersion.SelectedItem.ToString(), fileVersion.FileVersion);
            if(releaes != null)
            {
                HidePreReleaseCheckBox.Checked = !releaes.m_IsPreRelease;
                ModVersions.SelectedItem = releaes.m_ReleaseMeta.m_ReleaseName;
                m_CurrentInstalledVersion = releaes.m_EternalVersion;
            }
            else
            {
                GithubManager.AvalibleRelease lastRelease = GithubManager.GetLatestReleaseMeta(GameVersion.SelectedItem.ToString(), true);
                if(lastRelease != null)
                {
                    HidePreReleaseCheckBox.Checked = !lastRelease.m_IsPreRelease;
                    ModVersions.SelectedItem = lastRelease.m_ReleaseMeta.m_ReleaseName;
                    m_CurrentInstalledVersion = lastRelease.m_EternalVersion;
                }
            }
        }
        private void CheckingForInstalledMod(string gamePath)
        {
            if(File.Exists(gamePath + @"\Mods\SkyCoop.dll") && Directory.Exists(gamePath + @"\MelonLoader"))
            {
                m_ModReadyToInstall = true;
                m_ModAlreadyInstalled = true;
                UnInstallButton.Visible = true;
                UnInstallButton.Enabled = true;
                ChechingModVersion(gamePath);
            }
        }
        private bool CheckingGameVersion(string gamePath)
        {
            string gameVersionFile = gamePath + @"\tld_Data\StreamingAssets\version.txt";
            bool thisVersionIsCompatible = false;
            string version = " ";
            string selectedCompatibleVersion = string.Empty;

            if (File.Exists(gameVersionFile))
            {
                version = File.ReadAllText(gameVersionFile).Split(' ')[0];
                if(GameVersion.Items.Count > 0)
                {
                    foreach (string selectedVersion in GameVersion.Items)
                    {
                        if (selectedVersion.Contains("-"))
                        {
                            string[] versionRange = selectedVersion.Split('-');

                            int one = int.Parse(version.Split('.')[1]);
                            int tho = int.Parse(versionRange[0].Split('.')[1]);
                            int thre = int.Parse(versionRange[1].Split('.')[1]);
                            if ( one >= tho && one <= thre)
                            {
                                thisVersionIsCompatible = true;
                                selectedCompatibleVersion = selectedVersion;
                            }
                        }
                        else if(selectedVersion == version)
                        {
                            thisVersionIsCompatible = true;
                            selectedCompatibleVersion = selectedVersion;
                        }
                    }
                    if(selectedCompatibleVersion != string.Empty)
                    {
                        GameVersion.SelectedItem = selectedCompatibleVersion;
                    }
                }
            }

            if (thisVersionIsCompatible == false)
            {
                string Message;
                MessageBoxIcon Icon;
                if (string.IsNullOrEmpty(version) || version == " ")
                {
                    Message = "File you selected does not looks like The Long Dark. Make sure you selected tld.exe file, AND NOT SHORTCUT! You need to select tld.exe from the directory of the game.";
                    Icon = MessageBoxIcon.Error;
                    MessageBox.Show(Message, "ERROR", MessageBoxButtons.OK, Icon, MessageBoxDefaultButton.Button1);
                    return false;
                }
                else
                {
                    Message = $"The version of the game you have chosen ({version}), does not match with any supported builds of the game. You can select version manually, and continue on your own risk.";
                    Icon = MessageBoxIcon.Warning;
                    MessageBox.Show(Message, "Unknown version selected", MessageBoxButtons.OK, Icon, MessageBoxDefaultButton.Button1);
                }
            }

            return true;
        }
        private void FillGameVersionBox()
        {
            List<string> gameVersions = GithubManager.GetGameVersions();
            foreach(string version in gameVersions)
            {
                GameVersion.Items.Add(version);
            }
            GameVersion.SelectedItem = GameVersion.Items[0];
        }
        private void EnableNextButton(object sender, EventArgs e)
        {
            if(GameVersion.SelectedItem == null)
            {
                return;
            }
            NextButton.Enabled = true;
            ModVersions.Items.Clear();
            ModVersions.Enabled = true;
            m_GameVersionSelected = true;
        }
        private void UpdateReleaseList()
        {
            List<GithubManager.AvalibleRelease> avalibleReleases = GithubManager.GetReleasesForGameVersion(GameVersion.SelectedItem.ToString(), HidePreReleaseCheckBox.Checked);
            if (avalibleReleases != null)
            {
                ModVersions.Items.Clear();
                lastSelectetAvalibleReleases = avalibleReleases;
                foreach (GithubManager.AvalibleRelease release in avalibleReleases)
                {
                    ModVersions.Items.Add(release.m_ReleaseMeta.m_ReleaseName);
                }
                ModVersions.SelectedItem = ModVersions.Items[0];
            }
            UpdateReleaseInfo(this, EventArgs.Empty);
        }
        private void UpdateReleaseInfo(object sender, EventArgs e)
        {
            if(lastSelectetAvalibleReleases != null)
            {
                ChangeLogTextBox.Text = lastSelectetAvalibleReleases[ModVersions.SelectedIndex].m_ReleaseMeta.m_ChangeLog;
                ChangeLogLabel.Text = lastSelectetAvalibleReleases[ModVersions.SelectedIndex].m_ReleaseMeta.m_ReleaseName;
                InstallButton.Enabled = true;
                m_ModReadyToInstall = true;
            }
        }
        private void NextButton_Click(object sender, EventArgs e)
        {
            MainTabControl.SelectTab(1);
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MainTabControl.SelectedIndex == 0)
            {
                ChangeLogLabel.Visible = false;
                ChangeLogTextBox.Visible = false;
                NewsTextBox.Visible = true;
                InstallationLogTextBox.Visible = false;
            }
            else if (MainTabControl.SelectedIndex == 1)
            {
                ChangeLogLabel.Visible = true;
                ChangeLogTextBox.Visible = true;
                NewsTextBox.Visible = false;
                InstallationLogTextBox.Visible = false;
                if(m_ModAlreadyInstalled == true)
                {
                    InstallButton.Text = "UPDATE";
                    UnInstallButton.Enabled = true;
                }
                else
                {
                    InstallButton.Text = "INSTALL";
                    UnInstallButton.Enabled = false;
                }
            }
            else if (MainTabControl.SelectedIndex == 2)
            {
                ChangeLogLabel.Visible = false;
                ChangeLogTextBox.Visible = false;
                NewsTextBox.Visible = false;
                InstallationLogTextBox.Visible = true;
            }

            if (m_GameVersionSelected == false && m_ModReadyToInstall == false)
            {
                if(MainTabControl.SelectedIndex == 1 || MainTabControl.SelectedIndex == 2)
                {
                    MessageBox.Show("Please select game version first.",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
                    MainTabControl.SelectedIndex = 0;
                }
            }
            if(m_GameVersionSelected == true && m_ModInstalling == false) 
            {
                if (MainTabControl.SelectedIndex == 2)
                {
                    MessageBox.Show("Please select mod version first.",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
                    MainTabControl.SelectedIndex = 1;
                }
            }
        }

        private void HidePreReleaseCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            UpdateReleaseList();
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            if (m_ModReadyToInstall)
            {
                InstallationLogTextBox.Text = "";
                m_ModInstalling = true;
                MainTabControl.SelectedIndex = 2;

                GameVersion.Enabled = false;
                ModVersions.Enabled = false;
                SelectButton.Enabled = false;
                NextButton.Enabled = false;
                InstallButton.Enabled = false;
                HidePreReleaseCheckBox.Enabled = false;
                UnInstallButton.Enabled = false;

                GithubManager.AvalibleRelease release = lastSelectetAvalibleReleases[ModVersions.SelectedIndex];

                Downloader.Start(release, GamePath.Text, release.m_RequiredMelon);
            }
        }
        private void UnInstallButton_Click(object sender, EventArgs e)
        {
            if (m_ModAlreadyInstalled)
            {
                InstallationLogTextBox.Text = "";
                m_ModInstalling = true;
                MainTabControl.SelectedIndex = 2;

                GameVersion.Enabled = false;
                ModVersions.Enabled = false;
                SelectButton.Enabled = false;
                NextButton.Enabled = false;
                InstallButton.Enabled = false;
                HidePreReleaseCheckBox.Enabled = false;
                UnInstallButton.Enabled = false;

                GithubManager.AvalibleRelease release = GithubManager.GetReleasesByEternalVersion(GameVersion.SelectedItem.ToString(), m_CurrentInstalledVersion);

                bool fullUninstall = false;
                DialogResult result = MessageBox.Show("Yes - Delete all mods, plugins and MelonLoader (Game will be fully unmodified).\nNo - Delete only Sky Co-op and everything that was required for work of it. Other mods along with MelonLoader will not be touched.",
                    "Full uninstall?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);
                if(result == DialogResult.Yes)
                {
                    fullUninstall = true;
                }

                Uninstaller.Start(release, GamePath.Text, fullUninstall);
                m_ModAlreadyInstalled = false;
            }
        }
        public void SetMaximumValueProgressBar(int value)
        {
            TotalProggressBar.Maximum = value;
        }
        public void UpdateTotalProgressBar(int value)
        {
            TotalProggressBar.Value += value;
            TotalProggressLabel.Text =  "Total Progress: " + TotalProggressBar.Value.ToString() + "/" + TotalProggressBar.Maximum +" Files";
        }
        public void UpdateCurrentFileProgressBar(int value, string text)
        {
            CurrentFileProgessBar.Value = value;
            ProggressLabel.Text = $"{text} {value}%";
        }
        public void AddInstallLog(string Message)
        {
            if (string.IsNullOrEmpty(InstallationLogTextBox.Text))
            {
                InstallationLogTextBox.Text = Message;
            } else
            {
                InstallationLogTextBox.Text += "\r\n"+Message;
            }
        }
        public void ClearInstallLog()
        {
            InstallationLogTextBox.Text = string.Empty;
        }

        public void InstallFinished(string message, string version = "")
        {
            TotalProggressLabel.Text = "Total Progress: Done";
            ProggressLabel.Text = $"{message} completed";
            AddInstallLog($"{message} completed!");
            GameVersion.Enabled = true;
            ModVersions.Enabled = true;
            SelectButton.Enabled = true;
            NextButton.Enabled = true;
            InstallButton.Enabled = true;
            HidePreReleaseCheckBox.Enabled = true;
            TotalProggressBar.Value = 0;
            CurrentFileProgessBar.Value = 0;
            TotalProggressLabel.Text = "Total Proggress:";
            ProggressLabel.Text = "Proggress:";
            m_ModAlreadyInstalled = true;
            UnInstallButton.Visible = true;
            UnInstallButton.Enabled = true;

            MessageBox.Show($"{message} completed!",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.ServiceNotification);

            if(message == "Installation")
            {
                m_CurrentInstalledVersion = version;
            }
            else
            {
                m_CurrentInstalledVersion = string.Empty;
            }
        }
        private void CheckProgramVersion()
        {
            if (GithubManager.LatestInstallerVersion.Replace("\n", "") != BuildInfo.Version)
            {
                MessageBox.Show("A newer version of the installer is available, please download the latest version.\n(If you have already downloaded the latest version and you see this message, it means that the installer does not have Internet access.)",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                System.Diagnostics.Process.Start("https://github.com/RED1cat/SkyCoopInstaller/releases/latest");
                Environment.Exit(0);
            }
        }

        private void GithubIcon_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Filigrani/SkyCoop");
        }

        private void DiscordIcon_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/ydmufU2HHj");
        }

        private void Patreon_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.patreon.com/Filigrani");
        }

        private void BoostyIcon_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://boosty.to/filigrani");
        }
    }
}
