using System;
using System.Windows.Forms;
using System.IO;

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

            if(settings.CacheVideos)
            {
                changeCacheLocationButton.Enabled = true;
            }
            else
            {
                changeCacheLocationButton.Enabled = false;
            }
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
            settings.CacheLocation = txtCacheFolderPath.Text;

            settings.SaveSettings();
            
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
        }

        private void chkCacheVideos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCacheVideos.Checked)
            {
                changeCacheLocationButton.Enabled = true;
            }
            else
            {
                changeCacheLocationButton.Enabled = false;
            }
        }
    }
}
