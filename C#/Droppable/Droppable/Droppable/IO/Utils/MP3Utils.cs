using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Droppable.IO.Utils
{
    public static class MP3Utils
    {
        public const int KB_TO_BYTE = 1024;

        public static bool CheckMP3(SongInfo sf, string url)
        {
            var lowerURL = url.ToLower();

            if (SharedProperties.Settings.FilterRemixes && !sf.SongName.Contains("remix") && lowerURL.Contains("remix"))
                return false;

            if (SharedProperties.Settings.FilterCovers && CheckForCovers(sf, url))
                return false;

            if (true && !sf.SongName.Contains("live") && lowerURL.Contains("live"))
                return false;

            if (SharedProperties.Settings.FilterNightcore && !sf.SongName.Contains("nightcore") && lowerURL.Contains("nightcore"))
                return false;

            if ((SharedProperties.Settings.ExactMatch && (url.Contains(sf.SongName) && url.Contains(sf.ArtistName)))
                || (SharedProperties.Settings.PartialMatch && url.Contains(sf.SongName))
                || (!SharedProperties.Settings.PartialMatch && !SharedProperties.Settings.ExactMatch))
            {
                if (!string.IsNullOrEmpty(url) && url.Contains(".mp3") 
                    && new DroppableWebClient().DownloadDataGetLength(url) > SharedProperties.Settings.MinimumSizeKB * KB_TO_BYTE)
                    return true;
            }

            return false;
        }

        public static bool CheckMP3(string sf, string url)
        {
            var lowerURL = url.ToLower();

            if (SharedProperties.Settings.FilterRemixes && !sf.Contains("remix") && url.Contains("remix"))
                return false;

            if (SharedProperties.Settings.FilterCovers && CheckForCovers(sf, url))
                return false;

            if (SharedProperties.Settings.FilterNightcore && !sf.Contains("nightcore") && url.Contains("nightcore"))
                return false;

            if (!string.IsNullOrEmpty(url) && url.Contains(".mp3")
                && new DroppableWebClient().DownloadDataGetLength(url) > SharedProperties.Settings.MinimumSizeKB * KB_TO_BYTE)
                return true;

            return false;
        }

        private static bool CheckForCovers(string sf, string url)
        {
            var keywords = new string[]
            {
                "cover",
                "guitar",
                "drum",
                "vocal",
                "flute"
            };

            foreach (var word in keywords)
            {
                if (!sf.Contains(word) && url.Contains(word))
                    return true;
            }

            return false;
        }

        private static bool CheckForCovers(SongInfo sf, string url)
        {
            var keywords = new string[]
            {
                "cover",
                "guitar",
                "drum",
                "vocal",
                "flute"
            };

            foreach (var word in keywords)
            {
                if (!sf.SongName.Contains(word) && !sf.ArtistName.Contains(word) && url.Contains(word))
                    return true;
            }

            return false;
        }
    }
}
