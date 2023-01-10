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
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.metroTextBox1 = new MetroFramework.Controls.MetroTextBox();
            this.GameVersion = new MetroFramework.Controls.MetroComboBox();
            this.NextButton = new MetroFramework.Controls.MetroButton();
            this.SelectButton = new MetroFramework.Controls.MetroButton();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.NewsPanel = new MetroFramework.Controls.MetroPanel();
            ((System.ComponentModel.ISupportInitialize)(this.StyleManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SkyCoopLogo)).BeginInit();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
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
            // metroTabControl1
            // 
            this.metroTabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.metroTabPage3);
            this.metroTabControl1.FontSize = MetroFramework.MetroTabControlSize.Tall;
            this.metroTabControl1.FontWeight = MetroFramework.MetroTabControlWeight.Bold;
            this.metroTabControl1.ItemSize = new System.Drawing.Size(186, 34);
            this.metroTabControl1.Location = new System.Drawing.Point(20, 223);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(573, 384);
            this.metroTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.metroTabControl1.Style = MetroFramework.MetroColorStyle.Purple;
            this.metroTabControl1.TabIndex = 1;
            this.metroTabControl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTabControl1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTabControl1.UseStyleColors = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroTabPage1.Controls.Add(this.metroTextBox1);
            this.metroTabPage1.Controls.Add(this.GameVersion);
            this.metroTabPage1.Controls.Add(this.NextButton);
            this.metroTabPage1.Controls.Add(this.SelectButton);
            this.metroTabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(565, 342);
            this.metroTabPage1.Style = MetroFramework.MetroColorStyle.Purple;
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Select Game";
            this.metroTabPage1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            // 
            // metroTextBox1
            // 
            this.metroTextBox1.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.metroTextBox1.Location = new System.Drawing.Point(130, 30);
            this.metroTextBox1.Name = "metroTextBox1";
            this.metroTextBox1.Size = new System.Drawing.Size(409, 25);
            this.metroTextBox1.Style = MetroFramework.MetroColorStyle.Purple;
            this.metroTextBox1.TabIndex = 5;
            this.metroTextBox1.Text = "Select your TLD.exe directory";
            this.metroTextBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
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
            // metroTabPage2
            // 
            this.metroTabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(565, 342);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Select Mod Version";
            this.metroTabPage2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(565, 342);
            this.metroTabPage3.Style = MetroFramework.MetroColorStyle.Purple;
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Installation";
            this.metroTabPage3.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTabPage3.VerticalScrollbarBarColor = true;
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
            this.Controls.Add(this.metroTabControl1);
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
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Components.MetroStyleManager StyleManager;
        private System.Windows.Forms.PictureBox SkyCoopLogo;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private MetroFramework.Controls.MetroPanel NewsPanel;
        private MetroFramework.Controls.MetroTextBox metroTextBox1;
        private MetroFramework.Controls.MetroComboBox GameVersion;
        private MetroFramework.Controls.MetroButton NextButton;
        private MetroFramework.Controls.MetroButton SelectButton;
    }
}

