using System;
using System.Windows.Forms;
using System.IO;
using Aerial;
using System.Diagnostics;

namespace ScreenSaver
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        /// <summary>
        /// Load display text from the Registry
        /// </summary>
        private void LoadSettings()
        {
            var settings = new RegSettings();
            chkDifferentMonitorMovies.Checked = settings.DifferentMoviesOnDual;
            chkUseTimeOfDay.Checked = settings.UseTimeOfDay;
            chkMultiscreenDisabled.Checked = settings.MultiscreenDisabled;
            chkCacheVideos.Checked = settings.CacheVideos;

            if(settings.CacheLocation == null || settings.CacheLocation == "")
            {
                txtCacheFolderPath.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Aerial").ToString();
            }
            else
            {
                txtCacheFolderPath.Text = settings.CacheLocation;
            }
            
            changeCacheLocationButton.Enabled = settings.CacheVideos;

            ShowSpace();

            // while developing
            tabs.TabPages.Remove(tabAbout);
            grpChosenVideos.Hide();
        }

        private void ShowSpace()
        {
            var cacheSize = NativeMethods.GetExplorerFileSize(Caching.GetDirectorySize());
            lblCacheSize.Text = "Current Cache Size: " + cacheSize;

            var cacheFree = NativeMethods.GetExplorerFileSize(Caching.CacheSpace());
            lblFreeSpace.Text = "Free Space Available on drive: " + cacheFree;

        }

        /// <summary>
        /// Save text into the Registry.
        /// </summary>
        private void SaveSettings()
        {
            var settings = new RegSettings();
            settings.DifferentMoviesOnDual = chkDifferentMonitorMovies.Checked;
            settings.UseTimeOfDay = chkUseTimeOfDay.Checked;
            settings.MultiscreenDisabled = chkMultiscreenDisabled.Checked;
            settings.CacheVideos = chkCacheVideos.Checked;

            string oldCacheDirectory = settings.CacheLocation;
            settings.CacheLocation = txtCacheFolderPath.Text;

            settings.SaveSettings();

            Caching.UpdateCachePath(oldCacheDirectory, settings.CacheLocation);
        }


        private void okButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void changeCacheLocationButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = txtCacheFolderPath.Text;
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCacheFolderPath.Text = folderBrowserDialog.SelectedPath;
            }
            ShowSpace();
        }

        private void chkCacheVideos_CheckedChanged(object sender, EventArgs e)
        {
            changeCacheLocationButton.Enabled = chkCacheVideos.Checked;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            this.lblVersion.Text = "Current Version " + AssemblyVersion.ExecutingAssemblyVersion + " (" + AssemblyVersion.CompileDate + ")";
        }

        private void lblVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // todo get latest builds from json interface: https://api.github.com/repos/cdima/aerial/releases/latest
            ProcessStartInfo sInfo = new ProcessStartInfo("https://github.com/cDima/Aerial/releases");
            Process.Start(sInfo);
        }

        private void btnOpenCache_Click(object sender, EventArgs e)
        {
            Process.Start(Caching.CacheFolder);
        }

        private void btnPurgeCache_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete all cached files?", "Delete Cache?") == DialogResult.OK)
            {
                Caching.DeleteCache();
            }
            ShowSpace();
        }
    }
}
