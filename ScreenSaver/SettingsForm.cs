using System;
using System.Windows.Forms;

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
    }
}
