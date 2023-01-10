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
            this.GamePath = new MetroFramework.Controls.MetroTextBox();
            this.GameVersion = new MetroFramework.Controls.MetroComboBox();
            this.NextButton = new MetroFramework.Controls.MetroButton();
            this.SelectButton = new MetroFramework.Controls.MetroButton();
            this.SelectModVersionTab = new MetroFramework.Controls.MetroTabPage();
            this.InstallationTab = new MetroFramework.Controls.MetroTabPage();
            this.NewsPanel = new MetroFramework.Controls.MetroPanel();
            ((System.ComponentModel.ISupportInitialize)(this.StyleManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SkyCoopLogo)).BeginInit();
            this.TabControl1.SuspendLayout();
            this.SelectGameTab.SuspendLayout();
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
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(573, 384);
            this.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl1.Style = MetroFramework.MetroColorStyle.Purple;
            this.TabControl1.TabIndex = 1;
            this.TabControl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TabControl1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TabControl1.UseStyleColors = true;
            // 
            // SelectGameTab
            // 
            this.SelectGameTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SelectGameTab.Controls.Add(this.GamePath);
            this.SelectGameTab.Controls.Add(this.GameVersion);
            this.SelectGameTab.Controls.Add(this.NextButton);
            this.SelectGameTab.Controls.Add(this.SelectButton);
            this.SelectGameTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SelectGameTab.HorizontalScrollbarBarColor = true;
            this.SelectGameTab.Location = new System.Drawing.Point(4, 38);
            this.SelectGameTab.Name = "SelectGameTab";
            this.SelectGameTab.Size = new System.Drawing.Size(565, 342);
            this.SelectGameTab.Style = MetroFramework.MetroColorStyle.Purple;
            this.SelectGameTab.TabIndex = 0;
            this.SelectGameTab.Text = "Select Game";
            this.SelectGameTab.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.SelectGameTab.VerticalScrollbarBarColor = true;
            // 
            // GamePath
            // 
            this.GamePath.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.GamePath.Location = new System.Drawing.Point(130, 30);
            this.GamePath.Name = "GamePath";
            this.GamePath.Size = new System.Drawing.Size(409, 25);
            this.GamePath.Style = MetroFramework.MetroColorStyle.Purple;
            this.GamePath.TabIndex = 5;
            this.GamePath.Text = "Select your TLD.exe directory";
            this.GamePath.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // GameVersion
            // 
            this.GameVersion.FormattingEnabled = true;
            this.GameVersion.ItemHeight = 23;
            this.GameVersion.Location = new System.Drawing.Point(172, 81);
            this.GameVersion.Name = "GameVersion";
            this.GameVersion.Size = new System.Drawing.Size(294, 29);
            this.GameVersion.Style = MetroFramework.MetroColorStyle.Purple;
            this.GameVersion.TabIndex = 4;
            this.GameVersion.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(185, 219);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(173, 48);
            this.NextButton.Style = MetroFramework.MetroColorStyle.Purple;
            this.NextButton.TabIndex = 3;
            this.NextButton.Text = "NEXT";
            this.NextButton.Theme = MetroFramework.MetroThemeStyle.Dark;
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
            this.SelectModVersionTab.HorizontalScrollbarBarColor = true;
            this.SelectModVersionTab.Location = new System.Drawing.Point(4, 38);
            this.SelectModVersionTab.Name = "SelectModVersionTab";
            this.SelectModVersionTab.Size = new System.Drawing.Size(565, 342);
            this.SelectModVersionTab.TabIndex = 1;
            this.SelectModVersionTab.Text = "Select Mod Version";
            this.SelectModVersionTab.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.SelectModVersionTab.VerticalScrollbarBarColor = true;
            // 
            // InstallationTab
            // 
            this.InstallationTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InstallationTab.HorizontalScrollbarBarColor = true;
            this.InstallationTab.Location = new System.Drawing.Point(4, 38);
            this.InstallationTab.Name = "InstallationTab";
            this.InstallationTab.Size = new System.Drawing.Size(565, 342);
            this.InstallationTab.Style = MetroFramework.MetroColorStyle.Purple;
            this.InstallationTab.TabIndex = 2;
            this.InstallationTab.Text = "Installation";
            this.InstallationTab.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.InstallationTab.VerticalScrollbarBarColor = true;
            // 
            // NewsPanel
            // 
            this.NewsPanel.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.NewsPanel.HorizontalScrollbarBarColor = true;
            this.NewsPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.NewsPanel.HorizontalScrollbarSize = 10;
            this.NewsPanel.Location = new System.Drawing.Point(613, 33);
            this.NewsPanel.Name = "NewsPanel";
            this.NewsPanel.Size = new System.Drawing.Size(326, 570);
            this.NewsPanel.Style = MetroFramework.MetroColorStyle.Purple;
            this.NewsPanel.TabIndex = 2;
            this.NewsPanel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.NewsPanel.VerticalScrollbarBarColor = true;
            this.NewsPanel.VerticalScrollbarHighlightOnWheel = false;
            this.NewsPanel.VerticalScrollbarSize = 10;
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
    }
}

