using Microsoft.Win32;

namespace ScreenSaver
{
    public class RegSettings
    {
        string keyAddress = @"SOFTWARE\AerialScreenSaver";
        public bool DifferentMoviesOnDual = false;
        public bool UseTimeOfDay = true;

        public RegSettings()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(keyAddress);
            if (key != null)
            {
                DifferentMoviesOnDual = bool.Parse(key.GetValue(nameof(DifferentMoviesOnDual)) as string ?? "True");
                UseTimeOfDay = bool.Parse(key.GetValue(nameof(UseTimeOfDay)) as string ?? "True");
            }
        }

        /// <summary>
        /// Save text into the Registry.
        /// </summary>
        public void SaveSettings()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(keyAddress);
            
            key.SetValue(nameof(DifferentMoviesOnDual), DifferentMoviesOnDual);
            key.SetValue(nameof(UseTimeOfDay), UseTimeOfDay);
            
        }

    }
}
