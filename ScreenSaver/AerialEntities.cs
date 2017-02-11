using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace Aerial
{
    // Parses http://a1.phobos.apple.com/us/r1000/000/Features/atv/AutumnResources/videos/entries.json
    public class AerialContext
    {
        static List<Asset> cachedPlaylist;

        public List<Asset> GetMovies()
        {
            var entries = GetEntries();

            var urls = new JavaScriptSerializer().Deserialize<IdAsset[]>(entries);

            var time = (DateTime.Now.Hour < 6 || DateTime.Now.Hour > 19) ? "night" : "day";
            var ran = new Random();
            var settings = new RegSettings();
            var links = urls.SelectMany(s => s.assets)
                .OrderBy(t => ran.Next())
                .OrderByDescending(t => settings.UseTimeOfDay && t.timeOfDay == time)
                .ToList();

            if (settings.DifferentMoviesOnDual)
                return links;
            if (cachedPlaylist == null)
                cachedPlaylist = links;

            return cachedPlaylist;
        }

        private string GetEntries()
        {
            var aerialUrl = "http://a1.phobos.apple.com/us/r1000/000/Features/atv/AutumnResources/videos/entries.json";
#if OFFLINE
            aerialUrl = "http://BOGUS/entries.json";
#endif

            // update anyway
            Caching.StartDelayedCache(aerialUrl); 

            if (Caching.IsHit(aerialUrl))
                return File.ReadAllText(Caching.Get(aerialUrl));

            WebClient webClient = new WebClient();
            string entries = webClient.DownloadString(aerialUrl);
            return entries;
        }
    }

    public class IdAsset
    {
        public string id;
        public Asset[] assets;
    }

    public class Asset
    {
        public string url;//" : "http://a1.phobos.apple.com/us/r1000/000/Features/atv/AutumnResources/videos/b1-1.mov",
        public string accessibilityLabel;//" : "Hawaii",
        public string type;//" : "video",
        public string id;// : "b1-1",
        public string timeOfDay;//" : "day"
    }
}
