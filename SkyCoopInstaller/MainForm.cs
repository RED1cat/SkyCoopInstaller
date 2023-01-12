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
            GithubManager.PrepareReleasesList();
            NewsTextBox.Text = GithubManager.GetNews();
            TabControl1.SelectedIndex = 0;
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
            TabControl1.SelectTab(1);
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl1.SelectedIndex == 0)
            {
                ChangeLogLabel.Visible = false;
                ChangeLogTextBox.Visible = false;
                NewsTextBox.Visible = true;
                InstallationLogTextBox.Visible = false;
            }
            else if (TabControl1.SelectedIndex == 1)
            {
                ChangeLogLabel.Visible = true;
                ChangeLogTextBox.Visible = true;
                NewsTextBox.Visible = false;
                InstallationLogTextBox.Visible = false;
            }
            else if (TabControl1.SelectedIndex == 2)
            {
                ChangeLogLabel.Visible = false;
                ChangeLogTextBox.Visible = false;
                NewsTextBox.Visible = false;
                InstallationLogTextBox.Visible = true;
            }

            if (gameVersionSelected == false && modReadyToInstall == false)
            {
                if(TabControl1.SelectedIndex == 1 || TabControl1.SelectedIndex == 2)
                {
                    MessageBox.Show("Game version is not selected");
                    TabControl1.SelectedIndex = 0;
                }
            }
            if(gameVersionSelected == true && modInstalling == false) 
            {
                if (TabControl1.SelectedIndex == 2)
                {
                    MessageBox.Show("Installation has not started");
                    TabControl1.SelectedIndex = 1;
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
                TabControl1.SelectedIndex = 2;

                GameVersion.Enabled = false;
                ModVersions.Enabled = false;
                SelectButton.Enabled = false;
                NextButton.Enabled = false;
                InstallUninsallButton.Enabled = false;
                HidePreReleaseCheckBox.Enabled = false;

                GithubManager.AvalibleRelease releae = lastSelectetAvalibleReleases[ModVersions.SelectedIndex];
                TotalProggressBar.Maximum = releae.m_Dependencies.Count + 2;
                CurrentFileProgessBar.Maximum = 100;
                TotalProggressBar.ProgressBarStyle = ProgressBarStyle.Continuous;
                CurrentFileProgessBar.ProgressBarStyle = ProgressBarStyle.Continuous;
                Downloader.Start(releae, GamePath.Text, releae.m_RequiredMelon);
            }
        }
        public void UpdateTotalProgressBar(int value)
        {
            TotalProggressBar.Value += value;
            metroLabel1.Text =  "Total Progress: " + value + "/" + TotalProggressBar.Maximum +" Files";
        }
        public void UpdateCurrentFileProgressBar(int value)
        {
            CurrentFileProgessBar.Value = value;
            if(Downloader.CurrentlyDownloadingFile != null)
            {
                metroLabel2.Text = Downloader.CurrentlyDownloadingFile.m_FileName +" "+ value+"%";
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
            metroLabel1.Text = "Total Progress: Done";
            metroLabel2.Text = "Installation completed";
            AddInstallLog("Installation completed!");
            GameVersion.Enabled = true;
            ModVersions.Enabled = true;
            SelectButton.Enabled = true;
            NextButton.Enabled = true;
            InstallUninsallButton.Enabled = true;
            HidePreReleaseCheckBox.Enabled = true;
            MessageBox.Show("Installation completed!");
        }
    }
}
