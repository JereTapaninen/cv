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
    public class EngineRLSmp3 : Engine
    {
        public EngineRLSmp3() :
            base("RLSmp3", "http://www.rlsmp3.com", "/mp3/", 1)
        { }

        public override string Search(SongInfo info)
        {
            var pageContent = new DroppableWebClient().DownloadStringDispose(base.GetBaseURL() + base.GetSearchURL() + info.ToString() + ".html");

            var foundSongs = pageContent.Explode("target=\"_blank\" class=\"download_button\">Download</a> -->");

            var loopIndex = base.GetBaseIndex();

            while (true)
            {
                if (loopIndex >= foundSongs.Length)
                    break;

                //var currentSongURL = base.GetBaseURL() + foundSongs[loopIndex].Explode("\">Download</a>")[0];

                //var currentSongPageContent = new DroppableWebClient().DownloadStringDispose(currentSongURL);

                var downloadURL = foundSongs[loopIndex].Explode("<a rel=\"nofollow\" href=\"")[1].Explode("\" target=\"_blank\"")[0];

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
            var pageContent = new DroppableWebClient().DownloadStringDispose(base.GetBaseURL() + base.GetSearchURL() + searchQuery + ".html");

            var foundSongs = pageContent.Explode("target=\"_blank\" class=\"download_button\">Download</a> -->");

            var loopIndex = base.GetBaseIndex();

            while (true)
            {
                if (loopIndex >= foundSongs.Length)
                    break;

                //var currentSongURL = base.GetBaseURL() + foundSongs[loopIndex].Explode("\">Download</a>")[0];

                //var currentSongPageContent = new DroppableWebClient().DownloadStringDispose(currentSongURL);

                var downloadURL = foundSongs[loopIndex].Explode("<a rel=\"nofollow\" href=\"")[1].Explode("\" target=\"_blank\"")[0];

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
