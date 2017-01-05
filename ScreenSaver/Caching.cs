using System.IO;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Net;

public class Caching
{
    public static string TempFolder = "";
    public static string CacheFolder = new ScreenSaver.RegSettings().CacheLocation;

    public static int DelayAmount = 1000 * 10; // 10 seconds.
    
    /// <summary>
    /// Init cache. Clear partially downloaded files from temp folder.
    /// </summary>
    internal static void Setup()
    {
        // If there is no location stored in the Registry, use the default location
        if (CacheFolder == null)
        {
            CacheFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Aerial");
        }
        TempFolder = Path.Combine(CacheFolder, "temp");

        // Ensure folders exist
        Directory.CreateDirectory(CacheFolder);
        Directory.CreateDirectory(TempFolder);

        foreach (var file in Directory.CreateDirectory(TempFolder).GetFiles())
        {
            file.Delete();
        }
    }

    private static void OnDownloadFileComplete(object sender, AsyncCompletedEventArgs e)
    {
        var filename = e.UserState.ToString();
        var tempPath = Path.Combine(TempFolder, filename);
        if (e.Cancelled == false && e.Error == null)
        {
            Directory.Move(tempPath, Path.Combine(CacheFolder, filename));
        } else
        {
            // attempt to remove partially downloaded file
            File.Delete(filename);
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

    /// <summary>
    ///  Ensures the drive with user folder has more than 1 gig space left.
    /// </summary>
    /// <returns></returns>
    private static bool EnsureEnoughSpace()
    {
        foreach (var drive in DriveInfo.GetDrives())
        {
            if (CacheFolder.StartsWith(drive.Name))
                return drive.TotalFreeSpace > 1000000000;
        }
        return true; // ?
    }
}
