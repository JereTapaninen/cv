using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;

using Droppable.IO.Utils;

namespace Droppable.IO.Engines
{
    public class Enginemp3musicx : Engine
    {
        public Enginemp3musicx() :
            base("mp3musicx", "https://vertuha.xyz", "/", 1)
        { }

        public override string Search(SongInfo info)
        {
            var pageContent = new DroppableWebClient().DownloadStringDispose(base.GetBaseURL() + base.GetSearchURL() + info.ToString() + "/");

            var foundSongs = pageContent.Explode("<div class=\"download\"><a href=\"/go.php?url=");

            var loopIndex = base.GetBaseIndex();

            while (true)
            {
                if (loopIndex >= foundSongs.Length)
                    break;

                //var currentSongURL = base.GetBaseURL() + foundSongs[loopIndex].Explode("\">Download</a>")[0];

                //var currentSongPageContent = new DroppableWebClient().DownloadStringDispose(currentSongURL);

                var downloadURL = foundSongs[loopIndex].Explode("&sid=")[0];

                //var downloadURL = base.GetBaseURL() + currentSongPageContent.Explode("<audio autoplay=\"\" controls=\"\" width=\"120px\" height=\"30px\"><source src=\"")[1].Explode("\" type")[0];

                if (SharedProperties.Settings.FastSearch || MP3Utils.CheckMP3(info, downloadURL))
                {
                    return downloadURL;
                }

                loopIndex++;
            }

            return null;
        }

        public override string SearchDirty(string searchQuery)
        {
            var pageContent = new DroppableWebClient().DownloadStringDispose(base.GetBaseURL() + base.GetSearchURL() + searchQuery + "/");

            var foundSongs = pageContent.Explode("<div class=\"download\"><a href=\"/go.php?url=");

            var loopIndex = base.GetBaseIndex();

            while (true)
            {
                if (loopIndex >= foundSongs.Length)
                    break;

                //var currentSongURL = base.GetBaseURL() + foundSongs[loopIndex].Explode("\">Download</a>")[0];

                //var currentSongPageContent = new DroppableWebClient().DownloadStringDispose(currentSongURL);

                var downloadURL = foundSongs[loopIndex].Explode("&sid=")[0];

                //var downloadURL = base.GetBaseURL() + currentSongPageContent.Explode("<audio autoplay=\"\" controls=\"\" width=\"120px\" height=\"30px\"><source src=\"")[1].Explode("\" type")[0];

                if (SharedProperties.Settings.FastSearch || MP3Utils.CheckMP3(searchQuery, downloadURL))
                {
                    return downloadURL;
                }

                loopIndex++;
            }

            return null;
        }
    }
}
