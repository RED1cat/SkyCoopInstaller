using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
                    string[] Slices = ofd.FileName.Split('\\');
                    string LastSlice = Slices[Slices.Length - 1];
                    string RightPath = ofd.FileName.Replace(LastSlice,"");

                    GamePath.Text = RightPath;
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
                Downloader.Start(releae, GamePath.Text);
            }
        }
        public void UpdateTotalProgressBar(int value)
        {
            TotalProggressBar.Value = value;
        }
        public void UpdateCurrentFileProgressBar(int value)
        {
            CurrentFileProgessBar.Value = value;
        }
    }
}
