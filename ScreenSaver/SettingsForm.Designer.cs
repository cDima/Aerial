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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.lblVersion = new System.Windows.Forms.LinkLabel();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabPreferences = new System.Windows.Forms.TabPage();
            this.grpChosenVideos = new System.Windows.Forms.GroupBox();
            this.cbLivePreview = new System.Windows.Forms.CheckBox();
            this.player = new AxWMPLib.AxWindowsMediaPlayer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chkUseTimeOfDay = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkMultiscreenDisabled = new System.Windows.Forms.CheckBox();
            this.chkDifferentMonitorMovies = new System.Windows.Forms.CheckBox();
            this.tabCache = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numOfCurrDown_lbl = new System.Windows.Forms.Label();
            this.fullDownloadBtn = new System.Windows.Forms.Button();
            this.lblFreeSpace = new System.Windows.Forms.Label();
            this.btnPurgeCache = new System.Windows.Forms.Button();
            this.lblCacheSize = new System.Windows.Forms.Label();
            this.btnOpenCache = new System.Windows.Forms.Button();
            this.chkCacheVideos = new System.Windows.Forms.CheckBox();
            this.changeCacheLocationButton = new System.Windows.Forms.Button();
            this.txtCacheFolderPath = new System.Windows.Forms.TextBox();
            this.tabSource = new System.Windows.Forms.TabPage();
            this.videoSourceResetButton = new System.Windows.Forms.Button();
            this.lbl_VideoSourceURL = new System.Windows.Forms.Label();
            this.changeVideoSourceText = new System.Windows.Forms.TextBox();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.timerDiskUpdate = new System.Windows.Forms.Timer(this.components);
            this.tvChosen = new Aerial.Controls.EntitiesTreeView();
            this.tabs.SuspendLayout();
            this.tabPreferences.SuspendLayout();
            this.grpChosenVideos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabCache.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabSource.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(16, 473);
            this.okButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 28);
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
            this.cancelButton.Location = new System.Drawing.Point(124, 473);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 28);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.Location = new System.Drawing.Point(274, 476);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblVersion.Size = new System.Drawing.Size(284, 28);
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
            this.tabs.Controls.Add(this.tabSource);
            this.tabs.Controls.Add(this.tabAbout);
            this.tabs.Location = new System.Drawing.Point(16, 14);
            this.tabs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(547, 444);
            this.tabs.TabIndex = 14;
            // 
            // tabPreferences
            // 
            this.tabPreferences.Controls.Add(this.grpChosenVideos);
            this.tabPreferences.Controls.Add(this.groupBox1);
            this.tabPreferences.Location = new System.Drawing.Point(4, 25);
            this.tabPreferences.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPreferences.Name = "tabPreferences";
            this.tabPreferences.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPreferences.Size = new System.Drawing.Size(539, 415);
            this.tabPreferences.TabIndex = 0;
            this.tabPreferences.Text = "Preferences";
            this.tabPreferences.UseVisualStyleBackColor = true;
            // 
            // grpChosenVideos
            // 
            this.grpChosenVideos.Controls.Add(this.tvChosen);
            this.grpChosenVideos.Controls.Add(this.cbLivePreview);
            this.grpChosenVideos.Controls.Add(this.player);
            this.grpChosenVideos.Controls.Add(this.pictureBox1);
            this.grpChosenVideos.Controls.Add(this.chkUseTimeOfDay);
            this.grpChosenVideos.Location = new System.Drawing.Point(9, 92);
            this.grpChosenVideos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpChosenVideos.Name = "grpChosenVideos";
            this.grpChosenVideos.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpChosenVideos.Size = new System.Drawing.Size(519, 313);
            this.grpChosenVideos.TabIndex = 13;
            this.grpChosenVideos.TabStop = false;
            this.grpChosenVideos.Text = "Chosen Videos";
            // 
            // cbLivePreview
            // 
            this.cbLivePreview.AutoSize = true;
            this.cbLivePreview.Checked = true;
            this.cbLivePreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLivePreview.Location = new System.Drawing.Point(201, 256);
            this.cbLivePreview.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbLivePreview.Name = "cbLivePreview";
            this.cbLivePreview.Size = new System.Drawing.Size(109, 21);
            this.cbLivePreview.TabIndex = 18;
            this.cbLivePreview.Text = "Live Preview";
            this.cbLivePreview.UseVisualStyleBackColor = true;
            // 
            // player
            // 
            this.player.Enabled = true;
            this.player.Location = new System.Drawing.Point(151, 19);
            this.player.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.player.Name = "player";
            this.player.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("player.OcxState")));
            this.player.Size = new System.Drawing.Size(232, 132);
            this.player.TabIndex = 16;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::Aerial.Properties.Resources.surfacebook;
            this.pictureBox1.InitialImage = global::Aerial.Properties.Resources.surfacebook;
            this.pictureBox1.Location = new System.Drawing.Point(201, 38);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(309, 222);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // chkUseTimeOfDay
            // 
            this.chkUseTimeOfDay.AutoSize = true;
            this.chkUseTimeOfDay.Checked = true;
            this.chkUseTimeOfDay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseTimeOfDay.Location = new System.Drawing.Point(201, 284);
            this.chkUseTimeOfDay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkUseTimeOfDay.Name = "chkUseTimeOfDay";
            this.chkUseTimeOfDay.Size = new System.Drawing.Size(207, 21);
            this.chkUseTimeOfDay.TabIndex = 14;
            this.chkUseTimeOfDay.Text = "Prioritize current time of day";
            this.chkUseTimeOfDay.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkMultiscreenDisabled);
            this.groupBox1.Controls.Add(this.chkDifferentMonitorMovies);
            this.groupBox1.Location = new System.Drawing.Point(8, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(520, 78);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Multi-screen setups";
            // 
            // chkMultiscreenDisabled
            // 
            this.chkMultiscreenDisabled.AutoSize = true;
            this.chkMultiscreenDisabled.Checked = true;
            this.chkMultiscreenDisabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMultiscreenDisabled.Location = new System.Drawing.Point(8, 52);
            this.chkMultiscreenDisabled.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkMultiscreenDisabled.Name = "chkMultiscreenDisabled";
            this.chkMultiscreenDisabled.Size = new System.Drawing.Size(328, 21);
            this.chkMultiscreenDisabled.TabIndex = 14;
            this.chkMultiscreenDisabled.Text = "Show only on main screen on 3+ screen setups";
            this.chkMultiscreenDisabled.UseVisualStyleBackColor = true;
            // 
            // chkDifferentMonitorMovies
            // 
            this.chkDifferentMonitorMovies.AutoSize = true;
            this.chkDifferentMonitorMovies.Checked = true;
            this.chkDifferentMonitorMovies.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDifferentMonitorMovies.Location = new System.Drawing.Point(8, 23);
            this.chkDifferentMonitorMovies.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkDifferentMonitorMovies.Name = "chkDifferentMonitorMovies";
            this.chkDifferentMonitorMovies.Size = new System.Drawing.Size(253, 21);
            this.chkDifferentMonitorMovies.TabIndex = 12;
            this.chkDifferentMonitorMovies.Text = "Play different video on each screen";
            this.chkDifferentMonitorMovies.UseVisualStyleBackColor = true;
            // 
            // tabCache
            // 
            this.tabCache.Controls.Add(this.groupBox2);
            this.tabCache.Location = new System.Drawing.Point(4, 25);
            this.tabCache.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabCache.Name = "tabCache";
            this.tabCache.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabCache.Size = new System.Drawing.Size(539, 415);
            this.tabCache.TabIndex = 1;
            this.tabCache.Text = "Cache";
            this.tabCache.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numOfCurrDown_lbl);
            this.groupBox2.Controls.Add(this.fullDownloadBtn);
            this.groupBox2.Controls.Add(this.lblFreeSpace);
            this.groupBox2.Controls.Add(this.btnPurgeCache);
            this.groupBox2.Controls.Add(this.lblCacheSize);
            this.groupBox2.Controls.Add(this.btnOpenCache);
            this.groupBox2.Controls.Add(this.chkCacheVideos);
            this.groupBox2.Controls.Add(this.changeCacheLocationButton);
            this.groupBox2.Controls.Add(this.txtCacheFolderPath);
            this.groupBox2.Location = new System.Drawing.Point(9, 7);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(519, 398);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cache";
            // 
            // numOfCurrDown_lbl
            // 
            this.numOfCurrDown_lbl.AutoSize = true;
            this.numOfCurrDown_lbl.Location = new System.Drawing.Point(333, 57);
            this.numOfCurrDown_lbl.Name = "numOfCurrDown_lbl";
            this.numOfCurrDown_lbl.Size = new System.Drawing.Size(152, 17);
            this.numOfCurrDown_lbl.TabIndex = 21;
            this.numOfCurrDown_lbl.Text = "# of files downloading: ";
            // 
            // fullDownloadBtn
            // 
            this.fullDownloadBtn.Location = new System.Drawing.Point(308, 22);
            this.fullDownloadBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.fullDownloadBtn.Name = "fullDownloadBtn";
            this.fullDownloadBtn.Size = new System.Drawing.Size(203, 29);
            this.fullDownloadBtn.TabIndex = 20;
            this.fullDownloadBtn.Text = "Download All Videos";
            this.fullDownloadBtn.UseVisualStyleBackColor = true;
            this.fullDownloadBtn.Click += new System.EventHandler(this.fullDownloadBtn_Click);
            // 
            // lblFreeSpace
            // 
            this.lblFreeSpace.AutoSize = true;
            this.lblFreeSpace.Location = new System.Drawing.Point(21, 135);
            this.lblFreeSpace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFreeSpace.Name = "lblFreeSpace";
            this.lblFreeSpace.Size = new System.Drawing.Size(146, 17);
            this.lblFreeSpace.TabIndex = 19;
            this.lblFreeSpace.Text = "Free Space Available:";
            // 
            // btnPurgeCache
            // 
            this.btnPurgeCache.Location = new System.Drawing.Point(308, 129);
            this.btnPurgeCache.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPurgeCache.Name = "btnPurgeCache";
            this.btnPurgeCache.Size = new System.Drawing.Size(203, 28);
            this.btnPurgeCache.TabIndex = 18;
            this.btnPurgeCache.Text = "Delete Cache";
            this.btnPurgeCache.UseVisualStyleBackColor = true;
            this.btnPurgeCache.Click += new System.EventHandler(this.btnPurgeCache_Click);
            // 
            // lblCacheSize
            // 
            this.lblCacheSize.AutoSize = true;
            this.lblCacheSize.Location = new System.Drawing.Point(21, 106);
            this.lblCacheSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCacheSize.Name = "lblCacheSize";
            this.lblCacheSize.Size = new System.Drawing.Size(134, 17);
            this.lblCacheSize.TabIndex = 17;
            this.lblCacheSize.Text = "Current Cache Size:";
            // 
            // btnOpenCache
            // 
            this.btnOpenCache.Location = new System.Drawing.Point(308, 99);
            this.btnOpenCache.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenCache.Name = "btnOpenCache";
            this.btnOpenCache.Size = new System.Drawing.Size(203, 28);
            this.btnOpenCache.TabIndex = 16;
            this.btnOpenCache.Text = "Open Cache Location";
            this.btnOpenCache.UseVisualStyleBackColor = true;
            this.btnOpenCache.Click += new System.EventHandler(this.btnOpenCache_Click);
            // 
            // chkCacheVideos
            // 
            this.chkCacheVideos.AutoSize = true;
            this.chkCacheVideos.Location = new System.Drawing.Point(8, 23);
            this.chkCacheVideos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkCacheVideos.Name = "chkCacheVideos";
            this.chkCacheVideos.Size = new System.Drawing.Size(199, 21);
            this.chkCacheVideos.TabIndex = 13;
            this.chkCacheVideos.Text = "Cache videos while playing";
            this.chkCacheVideos.UseVisualStyleBackColor = true;
            // 
            // changeCacheLocationButton
            // 
            this.changeCacheLocationButton.Enabled = false;
            this.changeCacheLocationButton.Location = new System.Drawing.Point(308, 171);
            this.changeCacheLocationButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.changeCacheLocationButton.Name = "changeCacheLocationButton";
            this.changeCacheLocationButton.Size = new System.Drawing.Size(203, 28);
            this.changeCacheLocationButton.TabIndex = 15;
            this.changeCacheLocationButton.Text = "Change Cache Location...";
            this.changeCacheLocationButton.UseVisualStyleBackColor = true;
            this.changeCacheLocationButton.Click += new System.EventHandler(this.changeCacheLocationButton_Click);
            // 
            // txtCacheFolderPath
            // 
            this.txtCacheFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCacheFolderPath.Location = new System.Drawing.Point(8, 174);
            this.txtCacheFolderPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCacheFolderPath.Name = "txtCacheFolderPath";
            this.txtCacheFolderPath.ReadOnly = true;
            this.txtCacheFolderPath.Size = new System.Drawing.Size(290, 22);
            this.txtCacheFolderPath.TabIndex = 14;
            // 
            // tabSource
            // 
            this.tabSource.Controls.Add(this.videoSourceResetButton);
            this.tabSource.Controls.Add(this.lbl_VideoSourceURL);
            this.tabSource.Controls.Add(this.changeVideoSourceText);
            this.tabSource.Location = new System.Drawing.Point(4, 25);
            this.tabSource.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabSource.Name = "tabSource";
            this.tabSource.Size = new System.Drawing.Size(539, 415);
            this.tabSource.TabIndex = 3;
            this.tabSource.Text = "Video Source";
            this.tabSource.UseVisualStyleBackColor = true;
            // 
            // videoSourceResetButton
            // 
            this.videoSourceResetButton.Location = new System.Drawing.Point(15, 72);
            this.videoSourceResetButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.videoSourceResetButton.Name = "videoSourceResetButton";
            this.videoSourceResetButton.Size = new System.Drawing.Size(159, 27);
            this.videoSourceResetButton.TabIndex = 25;
            this.videoSourceResetButton.Text = "Reset Video Source";
            this.videoSourceResetButton.UseVisualStyleBackColor = true;
            this.videoSourceResetButton.Click += new System.EventHandler(this.videoSourceResetButton_Click);
            // 
            // lbl_VideoSourceURL
            // 
            this.lbl_VideoSourceURL.AutoSize = true;
            this.lbl_VideoSourceURL.Location = new System.Drawing.Point(12, 17);
            this.lbl_VideoSourceURL.Name = "lbl_VideoSourceURL";
            this.lbl_VideoSourceURL.Size = new System.Drawing.Size(373, 17);
            this.lbl_VideoSourceURL.TabIndex = 24;
            this.lbl_VideoSourceURL.Text = "Video Source URL (change requires restart to take effect)";
            // 
            // changeVideoSourceText
            // 
            this.changeVideoSourceText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.changeVideoSourceText.Location = new System.Drawing.Point(15, 45);
            this.changeVideoSourceText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.changeVideoSourceText.Name = "changeVideoSourceText";
            this.changeVideoSourceText.Size = new System.Drawing.Size(503, 22);
            this.changeVideoSourceText.TabIndex = 23;
            // 
            // tabAbout
            // 
            this.tabAbout.Location = new System.Drawing.Point(4, 25);
            this.tabAbout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Size = new System.Drawing.Size(539, 415);
            this.tabAbout.TabIndex = 2;
            this.tabAbout.Text = "About";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // timerDiskUpdate
            // 
            this.timerDiskUpdate.Enabled = true;
            this.timerDiskUpdate.Interval = 1000;
            this.timerDiskUpdate.Tick += new System.EventHandler(this.timerDiskUpdate_Tick);
            // 
            // tvChosen
            // 
            this.tvChosen.CheckBoxes = true;
            this.tvChosen.FullRowSelect = true;
            this.tvChosen.Location = new System.Drawing.Point(8, 23);
            this.tvChosen.Margin = new System.Windows.Forms.Padding(4);
            this.tvChosen.Name = "tvChosen";
            this.tvChosen.ShowLines = false;
            this.tvChosen.ShowPlusMinus = false;
            this.tvChosen.Size = new System.Drawing.Size(184, 281);
            this.tvChosen.TabIndex = 19;
            this.tvChosen.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvChosen_AfterSelect);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 516);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Aerial Settings";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabs.ResumeLayout(false);
            this.tabPreferences.ResumeLayout(false);
            this.grpChosenVideos.ResumeLayout(false);
            this.grpChosenVideos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabCache.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabSource.ResumeLayout(false);
            this.tabSource.PerformLayout();
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
        private System.Windows.Forms.Button btnOpenCache;
        private System.Windows.Forms.Label lblFreeSpace;
        private System.Windows.Forms.Button btnPurgeCache;
        private System.Windows.Forms.Label lblCacheSize;
        private AxWMPLib.AxWindowsMediaPlayer player;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox cbLivePreview;
        private System.Windows.Forms.Timer timerDiskUpdate;
        private Aerial.Controls.EntitiesTreeView tvChosen;
        private System.Windows.Forms.TabPage tabSource;
        private System.Windows.Forms.Button videoSourceResetButton;
        private System.Windows.Forms.Label lbl_VideoSourceURL;
        private System.Windows.Forms.TextBox changeVideoSourceText;
        private System.Windows.Forms.Button fullDownloadBtn;
        private System.Windows.Forms.Label numOfCurrDown_lbl;
    }
}
