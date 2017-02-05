namespace ScreenSaver
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.lblVersion = new System.Windows.Forms.LinkLabel();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabPreferences = new System.Windows.Forms.TabPage();
            this.tabCache = new System.Windows.Forms.TabPage();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.changeCacheLocationButton = new System.Windows.Forms.Button();
            this.txtCacheFolderPath = new System.Windows.Forms.TextBox();
            this.chkCacheVideos = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkMultiscreenDisabled = new System.Windows.Forms.CheckBox();
            this.chkDifferentMonitorMovies = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grpChosenVideos = new System.Windows.Forms.GroupBox();
            this.selectedVideos = new System.Windows.Forms.CheckedListBox();
            this.chkUseTimeOfDay = new System.Windows.Forms.CheckBox();
            this.tabs.SuspendLayout();
            this.tabPreferences.SuspendLayout();
            this.tabCache.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpChosenVideos.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 384);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.CausesValidation = false;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(93, 384);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.Location = new System.Drawing.Point(205, 384);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblVersion.Size = new System.Drawing.Size(213, 23);
            this.lblVersion.TabIndex = 13;
            this.lblVersion.TabStop = true;
            this.lblVersion.Text = "Version Info";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblVersion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblVersion_LinkClicked);
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Controls.Add(this.tabPreferences);
            this.tabs.Controls.Add(this.tabCache);
            this.tabs.Controls.Add(this.tabAbout);
            this.tabs.Location = new System.Drawing.Point(12, 12);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(410, 361);
            this.tabs.TabIndex = 14;
            // 
            // tabPreferences
            // 
            this.tabPreferences.Controls.Add(this.grpChosenVideos);
            this.tabPreferences.Controls.Add(this.groupBox1);
            this.tabPreferences.Location = new System.Drawing.Point(4, 22);
            this.tabPreferences.Name = "tabPreferences";
            this.tabPreferences.Padding = new System.Windows.Forms.Padding(3);
            this.tabPreferences.Size = new System.Drawing.Size(402, 335);
            this.tabPreferences.TabIndex = 0;
            this.tabPreferences.Text = "Preferences";
            this.tabPreferences.UseVisualStyleBackColor = true;
            // 
            // tabCache
            // 
            this.tabCache.Controls.Add(this.groupBox2);
            this.tabCache.Location = new System.Drawing.Point(4, 22);
            this.tabCache.Name = "tabCache";
            this.tabCache.Padding = new System.Windows.Forms.Padding(3);
            this.tabCache.Size = new System.Drawing.Size(402, 335);
            this.tabCache.TabIndex = 1;
            this.tabCache.Text = "Cache";
            this.tabCache.UseVisualStyleBackColor = true;
            // 
            // tabAbout
            // 
            this.tabAbout.Location = new System.Drawing.Point(4, 22);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Size = new System.Drawing.Size(402, 335);
            this.tabAbout.TabIndex = 2;
            this.tabAbout.Text = "About";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // changeCacheLocationButton
            // 
            this.changeCacheLocationButton.Enabled = false;
            this.changeCacheLocationButton.Location = new System.Drawing.Point(231, 40);
            this.changeCacheLocationButton.Name = "changeCacheLocationButton";
            this.changeCacheLocationButton.Size = new System.Drawing.Size(152, 23);
            this.changeCacheLocationButton.TabIndex = 15;
            this.changeCacheLocationButton.Text = "Change Cache Location...";
            this.changeCacheLocationButton.UseVisualStyleBackColor = true;
            // 
            // txtCacheFolderPath
            // 
            this.txtCacheFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCacheFolderPath.Enabled = false;
            this.txtCacheFolderPath.Location = new System.Drawing.Point(6, 42);
            this.txtCacheFolderPath.Name = "txtCacheFolderPath";
            this.txtCacheFolderPath.Size = new System.Drawing.Size(219, 20);
            this.txtCacheFolderPath.TabIndex = 14;
            // 
            // chkCacheVideos
            // 
            this.chkCacheVideos.AutoSize = true;
            this.chkCacheVideos.Location = new System.Drawing.Point(6, 19);
            this.chkCacheVideos.Name = "chkCacheVideos";
            this.chkCacheVideos.Size = new System.Drawing.Size(154, 17);
            this.chkCacheVideos.TabIndex = 13;
            this.chkCacheVideos.Text = "Cache videos while playing";
            this.chkCacheVideos.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkMultiscreenDisabled);
            this.groupBox1.Controls.Add(this.chkDifferentMonitorMovies);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 63);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Multi-screen setups";
            // 
            // chkMultiscreenDisabled
            // 
            this.chkMultiscreenDisabled.AutoSize = true;
            this.chkMultiscreenDisabled.Checked = true;
            this.chkMultiscreenDisabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMultiscreenDisabled.Location = new System.Drawing.Point(6, 42);
            this.chkMultiscreenDisabled.Name = "chkMultiscreenDisabled";
            this.chkMultiscreenDisabled.Size = new System.Drawing.Size(249, 17);
            this.chkMultiscreenDisabled.TabIndex = 14;
            this.chkMultiscreenDisabled.Text = "Show only on main screen on 3+ screen setups";
            this.chkMultiscreenDisabled.UseVisualStyleBackColor = true;
            // 
            // chkDifferentMonitorMovies
            // 
            this.chkDifferentMonitorMovies.AutoSize = true;
            this.chkDifferentMonitorMovies.Checked = true;
            this.chkDifferentMonitorMovies.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDifferentMonitorMovies.Location = new System.Drawing.Point(6, 19);
            this.chkDifferentMonitorMovies.Name = "chkDifferentMonitorMovies";
            this.chkDifferentMonitorMovies.Size = new System.Drawing.Size(193, 17);
            this.chkDifferentMonitorMovies.TabIndex = 12;
            this.chkDifferentMonitorMovies.Text = "Play different video on each screen";
            this.chkDifferentMonitorMovies.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkCacheVideos);
            this.groupBox2.Controls.Add(this.changeCacheLocationButton);
            this.groupBox2.Controls.Add(this.txtCacheFolderPath);
            this.groupBox2.Location = new System.Drawing.Point(7, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(389, 323);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cache";
            // 
            // grpChosenVideos
            // 
            this.grpChosenVideos.Controls.Add(this.chkUseTimeOfDay);
            this.grpChosenVideos.Controls.Add(this.selectedVideos);
            this.grpChosenVideos.Location = new System.Drawing.Point(7, 75);
            this.grpChosenVideos.Name = "grpChosenVideos";
            this.grpChosenVideos.Size = new System.Drawing.Size(389, 254);
            this.grpChosenVideos.TabIndex = 13;
            this.grpChosenVideos.TabStop = false;
            this.grpChosenVideos.Text = "Chosen Videos";
            // 
            // selectedVideos
            // 
            this.selectedVideos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedVideos.FormattingEnabled = true;
            this.selectedVideos.Location = new System.Drawing.Point(7, 19);
            this.selectedVideos.Name = "selectedVideos";
            this.selectedVideos.Size = new System.Drawing.Size(130, 229);
            this.selectedVideos.TabIndex = 1;
            // 
            // chkUseTimeOfDay
            // 
            this.chkUseTimeOfDay.AutoSize = true;
            this.chkUseTimeOfDay.Checked = true;
            this.chkUseTimeOfDay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseTimeOfDay.Location = new System.Drawing.Point(143, 19);
            this.chkUseTimeOfDay.Name = "chkUseTimeOfDay";
            this.chkUseTimeOfDay.Size = new System.Drawing.Size(210, 17);
            this.chkUseTimeOfDay.TabIndex = 14;
            this.chkUseTimeOfDay.Text = "Show appropriate day/night videos first";
            this.chkUseTimeOfDay.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 419);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Aerial Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabs.ResumeLayout(false);
            this.tabPreferences.ResumeLayout(false);
            this.tabCache.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpChosenVideos.ResumeLayout(false);
            this.grpChosenVideos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.LinkLabel lblVersion;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPreferences;
        private System.Windows.Forms.TabPage tabCache;
        private System.Windows.Forms.Button changeCacheLocationButton;
        private System.Windows.Forms.TextBox txtCacheFolderPath;
        private System.Windows.Forms.CheckBox chkCacheVideos;
        private System.Windows.Forms.TabPage tabAbout;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkMultiscreenDisabled;
        private System.Windows.Forms.CheckBox chkDifferentMonitorMovies;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox grpChosenVideos;
        private System.Windows.Forms.CheckBox chkUseTimeOfDay;
        private System.Windows.Forms.CheckedListBox selectedVideos;
    }
}
