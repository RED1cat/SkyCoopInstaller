namespace SkyCoopInstaller
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.StyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.SkyCoopLogo = new System.Windows.Forms.PictureBox();
            this.TabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.SelectGameTab = new MetroFramework.Controls.MetroTabPage();
            this.GameVersionLabel = new MetroFramework.Controls.MetroLabel();
            this.GamePath = new MetroFramework.Controls.MetroTextBox();
            this.GameVersion = new MetroFramework.Controls.MetroComboBox();
            this.NextButton = new MetroFramework.Controls.MetroButton();
            this.SelectButton = new MetroFramework.Controls.MetroButton();
            this.SelectModVersionTab = new MetroFramework.Controls.MetroTabPage();
            this.HidePreReleaseCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.InstallUninsallButton = new MetroFramework.Controls.MetroButton();
            this.ModVersions = new MetroFramework.Controls.MetroComboBox();
            this.AvalibleModVersion = new MetroFramework.Controls.MetroLabel();
            this.InstallationTab = new MetroFramework.Controls.MetroTabPage();
            this.NewsPanel = new MetroFramework.Controls.MetroPanel();
            this.ChangeLogTextBox = new MetroFramework.Controls.MetroTextBox();
            this.ChangeLogLabel = new MetroFramework.Controls.MetroLabel();
            this.NewsTextBox = new MetroFramework.Controls.MetroTextBox();
            this.GameVersionLabel = new MetroFramework.Controls.MetroLabel();
            this.ChangeLogTextBox = new MetroFramework.Controls.MetroTextBox();
            this.HidePreReleaseCheckBox = new MetroFramework.Controls.MetroCheckBox();
            this.TotalProggressBar = new MetroFramework.Controls.MetroProgressBar();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.CurrentFileProgessBar = new MetroFramework.Controls.MetroProgressBar();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.InstallationLogTextBox = new MetroFramework.Controls.MetroTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.StyleManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SkyCoopLogo)).BeginInit();
            this.TabControl1.SuspendLayout();
            this.SelectGameTab.SuspendLayout();
            this.SelectModVersionTab.SuspendLayout();
            this.InstallationTab.SuspendLayout();
            this.NewsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // StyleManager
            // 
            this.StyleManager.Owner = this;
            this.StyleManager.Style = MetroFramework.MetroColorStyle.Purple;
            this.StyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // SkyCoopLogo
            // 
            this.SkyCoopLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SkyCoopLogo.Image = global::SkyCoopInstaller.Properties.Resources.InstallerBanner1;
            this.SkyCoopLogo.Location = new System.Drawing.Point(20, 30);
            this.SkyCoopLogo.Name = "SkyCoopLogo";
            this.SkyCoopLogo.Size = new System.Drawing.Size(571, 177);
            this.SkyCoopLogo.TabIndex = 0;
            this.SkyCoopLogo.TabStop = false;
            // 
            // TabControl1
            // 
            this.TabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.TabControl1.Controls.Add(this.SelectGameTab);
            this.TabControl1.Controls.Add(this.SelectModVersionTab);
            this.TabControl1.Controls.Add(this.InstallationTab);
            this.TabControl1.FontSize = MetroFramework.MetroTabControlSize.Tall;
            this.TabControl1.FontWeight = MetroFramework.MetroTabControlWeight.Bold;
            this.TabControl1.ItemSize = new System.Drawing.Size(186, 34);
            this.TabControl1.Location = new System.Drawing.Point(20, 223);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 2;
            this.TabControl1.Size = new System.Drawing.Size(573, 343);
            this.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl1.Style = MetroFramework.MetroColorStyle.Purple;
            this.TabControl1.TabIndex = 1;
            this.TabControl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TabControl1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TabControl1.UseStyleColors = true;
            this.TabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // SelectGameTab
            // 
            this.SelectGameTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SelectGameTab.Controls.Add(this.GameVersionLabel);
            this.SelectGameTab.Controls.Add(this.GamePath);
            this.SelectGameTab.Controls.Add(this.GameVersion);
            this.SelectGameTab.Controls.Add(this.NextButton);
            this.SelectGameTab.Controls.Add(this.SelectButton);
            this.SelectGameTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SelectGameTab.HorizontalScrollbarBarColor = true;
            this.SelectGameTab.Location = new System.Drawing.Point(4, 38);
            this.SelectGameTab.Name = "SelectGameTab";
            this.SelectGameTab.Size = new System.Drawing.Size(565, 301);
            this.SelectGameTab.Style = MetroFramework.MetroColorStyle.Purple;
            this.SelectGameTab.TabIndex = 0;
            this.SelectGameTab.Text = "Select Game";
            this.SelectGameTab.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.SelectGameTab.VerticalScrollbarBarColor = true;
            // 
            // GameVersionLabel
            // 
            this.GameVersionLabel.AutoSize = true;
            this.GameVersionLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.GameVersionLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.GameVersionLabel.Location = new System.Drawing.Point(25, 85);
            this.GameVersionLabel.Name = "GameVersionLabel";
            this.GameVersionLabel.Size = new System.Drawing.Size(141, 25);
            this.GameVersionLabel.Style = MetroFramework.MetroColorStyle.Purple;
            this.GameVersionLabel.TabIndex = 6;
            this.GameVersionLabel.Text = "Game versions:";
            this.GameVersionLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // GamePath
            // 
            this.GamePath.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.GamePath.Location = new System.Drawing.Point(130, 30);
            this.GamePath.Name = "GamePath";
            this.GamePath.ReadOnly = true;
            this.GamePath.Size = new System.Drawing.Size(409, 25);
            this.GamePath.Style = MetroFramework.MetroColorStyle.Purple;
            this.GamePath.TabIndex = 5;
            this.GamePath.Text = "Select your TLD.exe directory";
            this.GamePath.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // GameVersion
            // 
            this.GameVersion.Enabled = false;
            this.GameVersion.FormattingEnabled = true;
            this.GameVersion.ItemHeight = 23;
            this.GameVersion.Location = new System.Drawing.Point(172, 81);
            this.GameVersion.Name = "GameVersion";
            this.GameVersion.Size = new System.Drawing.Size(294, 29);
            this.GameVersion.Style = MetroFramework.MetroColorStyle.Purple;
            this.GameVersion.TabIndex = 4;
            this.GameVersion.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.GameVersion.TextChanged += new System.EventHandler(this.EnableNextButton);
            // 
            // NextButton
            // 
            this.NextButton.Enabled = false;
            this.NextButton.Location = new System.Drawing.Point(196, 219);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(173, 48);
            this.NextButton.Style = MetroFramework.MetroColorStyle.Purple;
            this.NextButton.TabIndex = 3;
            this.NextButton.Text = "NEXT";
            this.NextButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // SelectButton
            // 
            this.SelectButton.Location = new System.Drawing.Point(27, 30);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(97, 23);
            this.SelectButton.Style = MetroFramework.MetroColorStyle.Purple;
            this.SelectButton.TabIndex = 2;
            this.SelectButton.Text = "SELECT";
            this.SelectButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.SelectButton.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // SelectModVersionTab
            // 
            this.SelectModVersionTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SelectModVersionTab.Controls.Add(this.HidePreReleaseCheckBox);
            this.SelectModVersionTab.Controls.Add(this.InstallUninsallButton);
            this.SelectModVersionTab.Controls.Add(this.ModVersions);
            this.SelectModVersionTab.Controls.Add(this.AvalibleModVersion);
            this.SelectModVersionTab.HorizontalScrollbarBarColor = true;
            this.SelectModVersionTab.Location = new System.Drawing.Point(4, 38);
            this.SelectModVersionTab.Name = "SelectModVersionTab";
            this.SelectModVersionTab.Size = new System.Drawing.Size(565, 301);
            this.SelectModVersionTab.Style = MetroFramework.MetroColorStyle.Purple;
            this.SelectModVersionTab.TabIndex = 1;
            this.SelectModVersionTab.Text = "Select Mod Version";
            this.SelectModVersionTab.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.SelectModVersionTab.VerticalScrollbarBarColor = true;
            // 
            // HidePreReleaseCheckBox
            // 
            this.HidePreReleaseCheckBox.AutoSize = true;
            this.HidePreReleaseCheckBox.Checked = true;
            this.HidePreReleaseCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HidePreReleaseCheckBox.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.HidePreReleaseCheckBox.Location = new System.Drawing.Point(224, 80);
            this.HidePreReleaseCheckBox.Name = "HidePreReleaseCheckBox";
            this.HidePreReleaseCheckBox.Size = new System.Drawing.Size(128, 19);
            this.HidePreReleaseCheckBox.Style = MetroFramework.MetroColorStyle.Purple;
            this.HidePreReleaseCheckBox.TabIndex = 7;
            this.HidePreReleaseCheckBox.Text = "Hide Pre-Release";
            this.HidePreReleaseCheckBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.HidePreReleaseCheckBox.UseVisualStyleBackColor = true;
            // 
            // InstallUninsallButton
            // 
            this.InstallUninsallButton.Enabled = false;
            this.InstallUninsallButton.Location = new System.Drawing.Point(196, 219);
            this.InstallUninsallButton.Name = "InstallUninsallButton";
            this.InstallUninsallButton.Size = new System.Drawing.Size(173, 48);
            this.InstallUninsallButton.Style = MetroFramework.MetroColorStyle.Purple;
            this.InstallUninsallButton.TabIndex = 6;
            this.InstallUninsallButton.Text = "INSTALL/UNINSTALL";
            this.InstallUninsallButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.InstallUninsallButton.Click += new System.EventHandler(this.InstallUninsallButton_Click);
            // 
            // ModVersions
            // 
            this.ModVersions.Enabled = false;
            this.ModVersions.FormattingEnabled = true;
            this.ModVersions.ItemHeight = 23;
            this.ModVersions.Location = new System.Drawing.Point(139, 45);
            this.ModVersions.Name = "ModVersions";
            this.ModVersions.Size = new System.Drawing.Size(294, 29);
            this.ModVersions.Style = MetroFramework.MetroColorStyle.Purple;
            this.ModVersions.TabIndex = 5;
            this.ModVersions.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ModVersions.TextChanged += new System.EventHandler(this.UpdateReleaseInfo);
            // 
            // AvalibleModVersion
            // 
            this.AvalibleModVersion.AutoSize = true;
            this.AvalibleModVersion.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.AvalibleModVersion.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.AvalibleModVersion.Location = new System.Drawing.Point(177, 17);
            this.AvalibleModVersion.Name = "AvalibleModVersion";
            this.AvalibleModVersion.Size = new System.Drawing.Size(214, 25);
            this.AvalibleModVersion.Style = MetroFramework.MetroColorStyle.Purple;
            this.AvalibleModVersion.TabIndex = 2;
            this.AvalibleModVersion.Text = "Available mod versions:";
            this.AvalibleModVersion.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // InstallationTab
            // 
            this.InstallationTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InstallationTab.Controls.Add(this.metroProgressSpinner1);
            this.InstallationTab.Controls.Add(this.metroLabel2);
            this.InstallationTab.Controls.Add(this.CurrentFileProgessBar);
            this.InstallationTab.Controls.Add(this.metroLabel1);
            this.InstallationTab.Controls.Add(this.TotalProggressBar);
            this.InstallationTab.HorizontalScrollbarBarColor = true;
            this.InstallationTab.Location = new System.Drawing.Point(4, 38);
            this.InstallationTab.Name = "InstallationTab";
            this.InstallationTab.Size = new System.Drawing.Size(565, 301);
            this.InstallationTab.Style = MetroFramework.MetroColorStyle.Purple;
            this.InstallationTab.TabIndex = 2;
            this.InstallationTab.Text = "Installation";
            this.InstallationTab.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.InstallationTab.VerticalScrollbarBarColor = true;
            // 
            // NewsPanel
            // 
            this.NewsPanel.Controls.Add(this.ChangeLogTextBox);
            this.NewsPanel.Controls.Add(this.ChangeLogLabel);
            this.NewsPanel.Controls.Add(this.NewsTextBox);
            this.NewsPanel.Controls.Add(this.InstallationLogTextBox);
            this.NewsPanel.HorizontalScrollbarBarColor = false;
            this.NewsPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.NewsPanel.HorizontalScrollbarSize = 0;
            this.NewsPanel.Location = new System.Drawing.Point(613, 33);
            this.NewsPanel.Name = "NewsPanel";
            this.NewsPanel.Size = new System.Drawing.Size(326, 570);
            this.NewsPanel.Style = MetroFramework.MetroColorStyle.Purple;
            this.NewsPanel.TabIndex = 2;
            this.NewsPanel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.NewsPanel.VerticalScrollbarBarColor = false;
            this.NewsPanel.VerticalScrollbarHighlightOnWheel = false;
            this.NewsPanel.VerticalScrollbarSize = 0;
            // 
            // ChangeLogTextBox
            // 
            this.ChangeLogTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ChangeLogTextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.ChangeLogTextBox.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.ChangeLogTextBox.Location = new System.Drawing.Point(0, 22);
            this.ChangeLogTextBox.Multiline = true;
            this.ChangeLogTextBox.Name = "ChangeLogTextBox";
            this.ChangeLogTextBox.ReadOnly = true;
            this.ChangeLogTextBox.Size = new System.Drawing.Size(326, 548);
            this.ChangeLogTextBox.Style = MetroFramework.MetroColorStyle.Purple;
            this.ChangeLogTextBox.TabIndex = 5;
            this.ChangeLogTextBox.Text = "ChangeLog";
            this.ChangeLogTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ChangeLogTextBox.UseStyleColors = true;
            this.ChangeLogTextBox.Visible = false;
            // 
            // ChangeLogLabel
            // 
            this.ChangeLogLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ChangeLogLabel.Location = new System.Drawing.Point(0, 0);
            this.ChangeLogLabel.Name = "ChangeLogLabel";
            this.ChangeLogLabel.Size = new System.Drawing.Size(326, 19);
            this.ChangeLogLabel.Style = MetroFramework.MetroColorStyle.Purple;
            this.ChangeLogLabel.TabIndex = 4;
            this.ChangeLogLabel.Text = "ChangeLog";
            this.ChangeLogLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ChangeLogLabel.Visible = false;
            // 
            // NewsTextBox
            // 
            this.NewsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NewsTextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.NewsTextBox.Location = new System.Drawing.Point(0, 0);
            this.NewsTextBox.Multiline = true;
            this.NewsTextBox.Name = "NewsTextBox";
            this.NewsTextBox.ReadOnly = true;
            this.NewsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NewsTextBox.Size = new System.Drawing.Size(326, 570);
            this.NewsTextBox.Style = MetroFramework.MetroColorStyle.Purple;
            this.NewsTextBox.TabIndex = 3;
            this.NewsTextBox.Text = "News";
            this.NewsTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.NewsTextBox.UseStyleColors = true;
            // 
            // GameVersionLabel
            // 
            this.GameVersionLabel.AutoSize = true;
            this.GameVersionLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.GameVersionLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.GameVersionLabel.Location = new System.Drawing.Point(25, 85);
            this.GameVersionLabel.Name = "GameVersionLabel";
            this.GameVersionLabel.Size = new System.Drawing.Size(141, 25);
            this.GameVersionLabel.Style = MetroFramework.MetroColorStyle.Purple;
            this.GameVersionLabel.TabIndex = 6;
            this.GameVersionLabel.Text = "Game versions:";
            this.GameVersionLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // ChangeLogTextBox
            // 
            this.ChangeLogTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ChangeLogTextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.ChangeLogTextBox.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.ChangeLogTextBox.Location = new System.Drawing.Point(0, 22);
            this.ChangeLogTextBox.Multiline = true;
            this.ChangeLogTextBox.Name = "ChangeLogTextBox";
            this.ChangeLogTextBox.ReadOnly = true;
            this.ChangeLogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChangeLogTextBox.Size = new System.Drawing.Size(326, 548);
            this.ChangeLogTextBox.Style = MetroFramework.MetroColorStyle.Purple;
            this.ChangeLogTextBox.TabIndex = 5;
            this.ChangeLogTextBox.Text = "ChangeLog";
            this.ChangeLogTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ChangeLogTextBox.UseStyleColors = true;
            this.ChangeLogTextBox.Visible = false;
            // 
            // HidePreReleaseCheckBox
            // 
            this.HidePreReleaseCheckBox.AutoSize = true;
            this.HidePreReleaseCheckBox.Checked = true;
            this.HidePreReleaseCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HidePreReleaseCheckBox.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.HidePreReleaseCheckBox.Location = new System.Drawing.Point(224, 80);
            this.HidePreReleaseCheckBox.Name = "HidePreReleaseCheckBox";
            this.HidePreReleaseCheckBox.Size = new System.Drawing.Size(128, 19);
            this.HidePreReleaseCheckBox.Style = MetroFramework.MetroColorStyle.Purple;
            this.HidePreReleaseCheckBox.TabIndex = 7;
            this.HidePreReleaseCheckBox.Text = "Hide Pre-Release";
            this.HidePreReleaseCheckBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.HidePreReleaseCheckBox.UseVisualStyleBackColor = true;
            this.HidePreReleaseCheckBox.CheckStateChanged += new System.EventHandler(this.HidePreReleaseCheckBox_CheckStateChanged);
            // 
            // TotalProggressBar
            // 
            this.TotalProggressBar.Location = new System.Drawing.Point(3, 273);
            this.TotalProggressBar.Maximum = 10;
            this.TotalProggressBar.Name = "TotalProggressBar";
            this.TotalProggressBar.ProgressBarStyle = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.TotalProggressBar.Size = new System.Drawing.Size(557, 23);
            this.TotalProggressBar.Step = 1;
            this.TotalProggressBar.Style = MetroFramework.MetroColorStyle.Magenta;
            this.TotalProggressBar.TabIndex = 2;
            this.TotalProggressBar.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel1
            // 
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(3, 245);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(557, 25);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Purple;
            this.metroLabel1.TabIndex = 3;
            this.metroLabel1.Text = "Total Proggress: 100%";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel1.UseStyleColors = true;
            // 
            // CurrentFileProgessBar
            // 
            this.CurrentFileProgessBar.Location = new System.Drawing.Point(3, 219);
            this.CurrentFileProgessBar.Maximum = 10;
            this.CurrentFileProgessBar.Name = "CurrentFileProgessBar";
            this.CurrentFileProgessBar.ProgressBarStyle = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.CurrentFileProgessBar.Size = new System.Drawing.Size(557, 23);
            this.CurrentFileProgessBar.Step = 1;
            this.CurrentFileProgessBar.Style = MetroFramework.MetroColorStyle.Magenta;
            this.CurrentFileProgessBar.TabIndex = 4;
            this.CurrentFileProgessBar.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroLabel2
            // 
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(3, 191);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(557, 25);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Purple;
            this.metroLabel2.TabIndex = 5;
            this.metroLabel2.Text = "Proggress:";
            this.metroLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel2.UseStyleColors = true;
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Location = new System.Drawing.Point(3, 3);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(557, 185);
            this.metroProgressSpinner1.Speed = 10F;
            this.metroProgressSpinner1.TabIndex = 6;
            this.metroProgressSpinner1.Value = 50;
            // 
            // InstallationLogTextBox
            // 
            this.InstallationLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InstallationLogTextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.InstallationLogTextBox.Location = new System.Drawing.Point(0, 0);
            this.InstallationLogTextBox.Multiline = true;
            this.InstallationLogTextBox.Name = "InstallationLogTextBox";
            this.InstallationLogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.InstallationLogTextBox.Size = new System.Drawing.Size(326, 570);
            this.InstallationLogTextBox.Style = MetroFramework.MetroColorStyle.Magenta;
            this.InstallationLogTextBox.TabIndex = 6;
            this.InstallationLogTextBox.Text = "metroTextBox1";
            this.InstallationLogTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.InstallationLogTextBox.UseStyleColors = true;
            this.InstallationLogTextBox.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 630);
            this.Controls.Add(this.NewsPanel);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.SkyCoopLogo);
            this.DisplayHeader = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this.StyleManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SkyCoopLogo)).EndInit();
            this.TabControl1.ResumeLayout(false);
            this.SelectGameTab.ResumeLayout(false);
            this.SelectGameTab.PerformLayout();
            this.SelectModVersionTab.ResumeLayout(false);
            this.SelectModVersionTab.PerformLayout();
            this.InstallationTab.ResumeLayout(false);
            this.NewsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Components.MetroStyleManager StyleManager;
        private System.Windows.Forms.PictureBox SkyCoopLogo;
        private MetroFramework.Controls.MetroTabControl TabControl1;
        private MetroFramework.Controls.MetroTabPage SelectGameTab;
        private MetroFramework.Controls.MetroTabPage SelectModVersionTab;
        private MetroFramework.Controls.MetroTabPage InstallationTab;
        private MetroFramework.Controls.MetroPanel NewsPanel;
        private MetroFramework.Controls.MetroTextBox GamePath;
        private MetroFramework.Controls.MetroComboBox GameVersion;
        private MetroFramework.Controls.MetroButton NextButton;
        private MetroFramework.Controls.MetroButton SelectButton;
        private MetroFramework.Controls.MetroButton InstallUninsallButton;
        private MetroFramework.Controls.MetroComboBox ModVersions;
        private MetroFramework.Controls.MetroLabel AvalibleModVersion;
        private MetroFramework.Controls.MetroTextBox NewsTextBox;
        private MetroFramework.Controls.MetroLabel ChangeLogLabel;
        private MetroFramework.Controls.MetroLabel GameVersionLabel;
        private MetroFramework.Controls.MetroTextBox ChangeLogTextBox;
        private MetroFramework.Controls.MetroCheckBox HidePreReleaseCheckBox;
        private MetroFramework.Controls.MetroProgressBar TotalProggressBar;
        private MetroFramework.Controls.MetroProgressBar CurrentFileProgessBar;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox InstallationLogTextBox;
    }
}

