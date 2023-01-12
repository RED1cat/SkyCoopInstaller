using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyCoopInstaller
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        List<GithubManager.AvalibleRelease> lastSelectetAvalibleReleases;
        bool gameVersionSelected = false;
        bool modReadyToInstall = false;
        bool modInstalling = false;
        bool modAlreadyInstalled = false;

        public MainForm()
        {
            InitializeComponent();
            if (DateTime.Now.Month == 12 || DateTime.Now.Month < 3)
            {
                SkyCoopLogo.Image = SkyCoopInstaller.Properties.Resources.InstallerBanner2;
            }
            GithubManager.PrepareReleasesList();
            NewsTextBox.Text = GithubManager.GetNews();
            CheckProgramVersion();
            MainTabControl.SelectedIndex = 0;
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
                    GamePath.Text = Directory.GetParent(ofd.FileName).FullName;
                    GameVersion.Enabled = true;
                    FillGameVersionBox();
                }
            }
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
            gameVersionSelected = true;

            UpdateReleaseList();
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

                if (modAlreadyInstalled == false)
                {
                    InstallUninsallButton.Text = "INSTALL";
                }
            }
            UpdateReleaseInfo(this, EventArgs.Empty);
        }
        private void UpdateReleaseInfo(object sender, EventArgs e)
        {
            if(lastSelectetAvalibleReleases != null)
            {
                ChangeLogTextBox.Text = lastSelectetAvalibleReleases[ModVersions.SelectedIndex].m_ReleaseMeta.m_ChangeLog;
                ChangeLogLabel.Text = lastSelectetAvalibleReleases[ModVersions.SelectedIndex].m_ReleaseMeta.m_ReleaseName;
                InstallUninsallButton.Enabled = true;
                modReadyToInstall = true;
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
            }
            else if (MainTabControl.SelectedIndex == 2)
            {
                ChangeLogLabel.Visible = false;
                ChangeLogTextBox.Visible = false;
                NewsTextBox.Visible = false;
                InstallationLogTextBox.Visible = true;
            }

            if (gameVersionSelected == false && modReadyToInstall == false)
            {
                if(MainTabControl.SelectedIndex == 1 || MainTabControl.SelectedIndex == 2)
                {
                    MessageBox.Show("Please select game version first.");
                    MainTabControl.SelectedIndex = 0;
                }
            }
            if(gameVersionSelected == true && modInstalling == false) 
            {
                if (MainTabControl.SelectedIndex == 2)
                {
                    MessageBox.Show("Please select mod version first.");
                    MainTabControl.SelectedIndex = 1;
                }
            }
        }

        private void HidePreReleaseCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            UpdateReleaseList();
        }

        private void InstallUninsallButton_Click(object sender, EventArgs e)
        {
            if (modReadyToInstall)
            {
                modInstalling = true;
                MainTabControl.SelectedIndex = 2;

                GameVersion.Enabled = false;
                ModVersions.Enabled = false;
                SelectButton.Enabled = false;
                NextButton.Enabled = false;
                InstallUninsallButton.Enabled = false;
                HidePreReleaseCheckBox.Enabled = false;

                GithubManager.AvalibleRelease releae = lastSelectetAvalibleReleases[ModVersions.SelectedIndex];
                TotalProggressBar.Maximum = releae.m_Dependencies.Count + 2;

                Downloader.Start(releae, GamePath.Text, releae.m_RequiredMelon);
            }
        }
        public void UpdateTotalProgressBar(int value)
        {
            TotalProggressBar.Value += value;
            TotalProggressLabel.Text =  "Total Progress: " + TotalProggressBar.Value.ToString() + "/" + TotalProggressBar.Maximum +" Files";
        }
        public void UpdateCurrentFileProgressBar(int value)
        {
            CurrentFileProgessBar.Value = value;
            if(Downloader.CurrentlyDownloadingFile != null)
            {
                ProggressLabel.Text = Downloader.CurrentlyDownloadingFile.m_FileName +" "+ value+"%";
            }
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

        public void InstallFinished()
        {
            TotalProggressLabel.Text = "Total Progress: Done";
            ProggressLabel.Text = "Installation completed";
            AddInstallLog("Installation completed!");
            GameVersion.Enabled = true;
            ModVersions.Enabled = true;
            SelectButton.Enabled = true;
            NextButton.Enabled = true;
            InstallUninsallButton.Enabled = true;
            HidePreReleaseCheckBox.Enabled = true;
            TotalProggressBar.Value = 0;
            CurrentFileProgessBar.Value = 0;
            TotalProggressLabel.Text = "Total Proggress:";
            ProggressLabel.Text = "Proggress:";

            MessageBox.Show("Installation completed!");
        }
        private void CheckProgramVersion()
        {
            if (GithubManager.LatestInstallerVersion.Replace("\n", "") != BuildInfo.Version)
            {
                MessageBox.Show("A newer version of the installer is available, please download the latest version.");
                System.Diagnostics.Process.Start("https://github.com/RED1cat/SkyCoopInstaller/releases/latest");
                this.Close();
            }
        }
    }
}
