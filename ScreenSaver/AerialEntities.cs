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
        static IdAsset[] cachedEntities;
        static List<Asset> cachedPlaylist;

        public static List<Asset> GetMovies()
        {
            var urls = GetAllEntries();

            return FilterEntries(urls);
        }
        public static List<Asset> GetAllMovies()
        {
            return GetAllEntries().SelectMany(s => s.assets).ToList();
        }

        private static List<Asset> FilterEntries(IdAsset[] urls)
        {
            var time = (DateTime.Now.Hour < 6 || DateTime.Now.Hour > 19) ? "night" : "day";
            var ran = new Random();
            var settings = new RegSettings();
            List<Asset> links = urls.SelectMany(s => s.assets)
                .OrderBy(t => ran.Next()) // randomize
                .OrderByDescending(t => settings.UseTimeOfDay && t.timeOfDay == time)
                .ToList();

            if (settings.DifferentMoviesOnDual)
                return links;

            if (cachedPlaylist == null)
                cachedPlaylist = links;

            return cachedPlaylist;
        }

        public static IdAsset[] GetAllEntries()
        {
            if (cachedEntities != null) return cachedEntities;
            var aerialUrl = "http://a1.phobos.apple.com/us/r1000/000/Features/atv/AutumnResources/videos/entries.json";
#if OFFLINE
            aerialUrl = "http://BOGUS/entries.json";
#endif

            // update anyway
            Caching.StartDelayedCache(aerialUrl);

            string entries = "";
            if (Caching.IsHit(aerialUrl)) {
                entries = File.ReadAllText(Caching.Get(aerialUrl));
            } else {
                WebClient webClient = new WebClient();
                entries = webClient.DownloadString(aerialUrl);
            }
            cachedEntities = new JavaScriptSerializer().Deserialize<IdAsset[]>(entries);

            return cachedEntities;
        }
    }

    public class IdAsset
    {
        public string id;
        public Asset[] assets;
    }

    public class Asset : IComparable<Asset>
    {
        public string url;//" : "http://a1.phobos.apple.com/us/r1000/000/Features/atv/AutumnResources/videos/b1-1.mov",
        public string accessibilityLabel;//" : "Hawaii",
        public string type;//" : "video",
        public string id;// : "b1-1",
        public string timeOfDay;//" : "day"

        [NonSerialized]
        internal int numeric = 0;
        
        public override string ToString()
        {
            return accessibilityLabel + (numeric == 0 ? "" : " " + numeric) + " " + timeOfDay + "";
        }
        public string ShortName()
        {
            return accessibilityLabel + " — " + timeOfDay;
        }
        public string ToFullName()
        {
            return accessibilityLabel + " — " + timeOfDay + " (" + id + ")";
        }
        public string TimeNumbered()
        {
            return timeOfDay + (numeric == 0 ? "" : " " + numeric);
        }

        public int CompareTo(Asset other)
        {
            if (other == null) return 1;
            if (other == this) return 0;
            return NativeMethods.StrCmpLogicalW(ToFullName(), other.ToFullName());
        }
    }
}
