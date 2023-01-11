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
        bool modAlreadyInstalled = false;

        public MainForm()
        {
            InitializeComponent();
            GithubManager.PrepareReleasesList();
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
                    GamePath.Text = ofd.FileName;
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

            List<GithubManager.AvalibleRelease> avalibleReleases = GithubManager.GetReleasesForGameVersion(GameVersion.SelectedItem.ToString());
            if(avalibleReleases != null)
            {
                lastSelectetAvalibleReleases = avalibleReleases;
                foreach (GithubManager.AvalibleRelease release in avalibleReleases)
                {
                    ModVersions.Items.Add(release.m_ReleaseMeta.m_ReleaseName);
                }
                ModVersions.SelectedItem = ModVersions.Items[0];

                if(modAlreadyInstalled == false)
                {
                    InstallUninsallButton.Text = "INSTALL";
                }
            }
        }
        private void UpdateReleaseInfo(object sender, EventArgs e)
        {
            if(lastSelectetAvalibleReleases != null)
            {
                ChangeLogTextBox.Text = lastSelectetAvalibleReleases[ModVersions.SelectedIndex].m_ReleaseMeta.m_ChangeLog;
                ChangeLogLabel.Text = lastSelectetAvalibleReleases[ModVersions.SelectedIndex].m_ReleaseMeta.m_ReleaseName;
                InstallUninsallButton.Enabled = true;
            }
        }
        private void NextButton_Click(object sender, EventArgs e)
        {
            TabControl1.SelectTab(1);
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(gameVersionSelected == false && modReadyToInstall == false)
            {
                if(TabControl1.SelectedIndex == 1 || TabControl1.SelectedIndex == 2)
                {
                    MessageBox.Show("Game version is not selected");
                    TabControl1.SelectedIndex = 0;
                }
            }
            if(gameVersionSelected == true && modReadyToInstall == false) 
            {
                if (TabControl1.SelectedIndex == 2)
                {
                    MessageBox.Show("Installation has not started");
                    TabControl1.SelectedIndex = 1;
                }
            }
            if(TabControl1.SelectedIndex == 0)
            {
                ChangeLogLabel.Visible = false;
                ChangeLogTextBox.Visible = false;
                NewsTextBox.Visible = true;
            }
            if (TabControl1.SelectedIndex == 1)
            {
                ChangeLogLabel.Visible = true;
                ChangeLogTextBox.Visible = true;
                NewsTextBox.Visible = false;
            }
        }
    }
}
