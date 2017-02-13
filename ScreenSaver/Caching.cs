using System.IO;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;

namespace Aerial
{
    public class Caching
    {
        public static string TempFolder = "";
        public static string CacheFolder = new RegSettings().CacheLocation;

        public static int DelayAmount = 1000 * 10; // 10 seconds.

        /// <summary>
        /// Init cache. Clear partially downloaded files from temp folder.
        /// </summary>
        internal static void Setup()
        {
            // If there is no location stored in the Registry, use the default location
            if (string.IsNullOrWhiteSpace(CacheFolder))
            {
                CacheFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Aerial");
            }
            TempFolder = Path.Combine(CacheFolder, "temp");

            // Ensure folders exist
            Directory.CreateDirectory(CacheFolder);
            Directory.CreateDirectory(TempFolder);

            // Delete partial temp files if any 
            foreach (var file in Directory.CreateDirectory(TempFolder).GetFiles())
            {
                file.Delete();
            }
        }
        
        internal static bool IsHit(string url)
        {
            string filename = Path.GetFileName(url);
            return File.Exists(Path.Combine(CacheFolder, filename));
        }

        internal static bool IsCaching(string url)
        {
            string filename = Path.GetFileName(url);
            return File.Exists(Path.Combine(TempFolder, filename));
        }

        internal static string Get(string url)
        {
            string filename = Path.GetFileName(url);
            return Path.Combine(CacheFolder, filename);
        }

        internal static void StartDelayedCache(string url)
        {
            if (EnsureEnoughSpace())
            {
                Task.Delay(DelayAmount).ContinueWith(t =>
                {
                    if (!IsCaching(url))
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadFileComplete);
                            string filename = Path.GetFileName(url);
                            client.DownloadFileAsync(new Uri(url), Path.Combine(TempFolder, filename), filename);
                        }
                    }
                });
            }
        }
        private static void OnDownloadFileComplete(object sender, AsyncCompletedEventArgs e)
        {
            var filename = e.UserState.ToString();
            var tempFullPath = Path.Combine(TempFolder, filename);
            var cacheFullpath = Path.Combine(CacheFolder, filename);
            if (e.Cancelled == false && e.Error == null)
            {
                // delete if old file exists
                if (File.Exists(cacheFullpath))
                    File.Delete(cacheFullpath);

                Directory.Move(tempFullPath, cacheFullpath);
            }
            else
            {
                // attempt to remove partially downloaded file
                File.Delete(tempFullPath);
            }
        }

        internal static async void UpdateCachePath(string oldCacheDirectory, string cacheLocation)
        {
            CacheFolder = cacheLocation;

            // Move old cache to new location if space allows
            var currentCacheSpace = GetDirectorySize(oldCacheDirectory);
            if (currentCacheSpace < CacheSpace() - (1000 * 1000 * 1000))
            {
                // Note might take a while, hanging the save dialog
                // video blocks this command: Directory.Move(oldCacheDirectory, cacheLocation);
                foreach(var f in Directory.GetFiles(oldCacheDirectory))
                {
                    var newfile = Path.Combine(cacheLocation, Path.GetFileName(f));
                    if (!File.Exists(newfile))
                        await Task.Factory.StartNew(() => File.Move(f, newfile));
                    
                }
            }

            DeleteCache(oldCacheDirectory);

            // Delete old cache
            try
            {
                await Task.Factory.StartNew(() => Directory.Delete(oldCacheDirectory, true));
            } catch(UnauthorizedAccessException)
            {
                // Leave dir for now.
                // todo - windows removes all files after the video player stops using them,
                // yet leaves the folder, we need to redo this operation in 3 mins, for example.
            }
        }

        public static async void DeleteCache(string folder = null)
        {
            if (folder == null) folder = CacheFolder;
            foreach (var f in Directory.GetFiles(folder))
            {
                try
                {
                    await Task.Factory.StartNew(() => File.Delete(f));
                }
                catch (UnauthorizedAccessException ex)
                {
                    // video may be used while deleting
                    Trace.WriteLine("Access denied while moving cached files " + ex);
                }
            }
        }

        public static long CacheSpace()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (CacheFolder.StartsWith(drive.Name))
                    return drive.TotalFreeSpace;
            }
            return 0;
        }
        
        public static long GetDirectorySize(string path = null)
        {
            if (path == null) path = CacheFolder;
            long size = 0;
            foreach (string name in Directory.GetFiles(path, "*.*"))
                size += new FileInfo(name).Length;

            return size;
        }

        /// <summary>
        ///  Ensures the drive with user folder has more than 1 gig space left.
        /// </summary>
        /// <returns></returns>
        private static bool EnsureEnoughSpace()
        {
            return CacheSpace() > 1000000000;
        }
    }
}