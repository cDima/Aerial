using System.IO;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Net;

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
            if (CacheFolder == null || CacheFolder == "")
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

        public static long CacheSpace()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (CacheFolder.StartsWith(drive.Name))
                    return drive.TotalFreeSpace;
            }
            return 0;
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