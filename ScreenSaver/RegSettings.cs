using Microsoft.Win32;
using System;
using System.IO;

namespace Aerial
{
    public class RegSettings
    {
        readonly string keyAddress = @"SOFTWARE\AerialScreenSaver";
        public bool DifferentMoviesOnDual = false;
        public bool UseTimeOfDay = true;
        public bool MultiscreenDisabled = true;
        public bool CacheVideos = true;
        public string CacheLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Aerial");

        public RegSettings()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(keyAddress);
            if (key != null)
            {
                DifferentMoviesOnDual = bool.Parse(key.GetValue(nameof(DifferentMoviesOnDual)) as string ?? "True");
                UseTimeOfDay = bool.Parse(key.GetValue(nameof(UseTimeOfDay)) as string ?? "True");
                MultiscreenDisabled = bool.Parse(key.GetValue(nameof(MultiscreenDisabled)) as string ?? "True");
                CacheVideos = bool.Parse(key.GetValue(nameof(CacheVideos)) as string ?? "True");
                CacheLocation = key.GetValue(nameof(CacheLocation)) as string;
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
            key.SetValue(nameof(MultiscreenDisabled), MultiscreenDisabled);
            key.SetValue(nameof(CacheVideos), CacheVideos);
            key.SetValue(nameof(CacheLocation), CacheLocation);
        }

    }
}
