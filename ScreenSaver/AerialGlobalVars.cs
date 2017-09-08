using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerial
{
    /**
     * Class for holding global variables such as URLs to the github repo for easy changing.  Origionally had them
     * in a app.config file, but that requires the config file to be in the same directory as the executable, 
     * so the install instructions would have to be changed
     */
    static class AerialGlobalVars
    {
        // URL for the JSON document that describes the latest github release
        public static string githubLatestReleaseDetails = "https://api.github.com/repos/cdima/aerial/releases/latest";
        //Link to the github releases page
        public static string githubAllReleases = "https://github.com/cDima/Aerial/releases";
        //link to the apple videos
        public static string appleVideosURI = "http://a1.phobos.apple.com/us/r1000/000/Features/atv/AutumnResources/videos/entries.json";
    }
}
