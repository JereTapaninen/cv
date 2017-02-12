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
    public class EngineShareLagu : Engine
    {
        public EngineShareLagu() :
            base("ShareLagu", "http://sharelagu.info", "/site_fullsearch.xhtml?get-q=", 1)
        { }

        public override string Search(SongInfo info)
        {
            var pageContent = new DroppableWebClient().DownloadStringDispose(base.GetBaseURL() + base.GetSearchURL() + info.ToString().Replace("-", "+"));

            var foundSongs = pageContent.Explode("<a class=\"dlink\" href=\"");

            var loopIndex = base.GetBaseIndex();

            while (true)
            {
                if (loopIndex >= foundSongs.Length)
                    break;

                var currentSongURL = base.GetBaseURL() + foundSongs[loopIndex].Explode("\">Download</a>")[0];

                var currentSongPageContent = new DroppableWebClient().DownloadStringDispose(currentSongURL);

                var downloadURL = string.Empty;

                if (currentSongPageContent.Contains("<audio autoplay=\"\" controls=\"\" width=\"120px\" height=\"30px\"><source src=\"")
                    && currentSongPageContent.Contains("\" type"))
                {
                    downloadURL = base.GetBaseURL()
                        + currentSongPageContent.Explode("<audio autoplay=\"\" controls=\"\" width=\"120px\" height=\"30px\"><source src=\"")[1].Explode("\" type")[0];
                }
                else
                {
                    downloadURL = base.GetBaseURL() + "/" + currentSongPageContent.Explode("\" class=\"glink\">Free Download</a> - <a href=\"")[1].Explode("\" class=")[0];
                }

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
            var pageContent = new DroppableWebClient().DownloadStringDispose(base.GetBaseURL() + base.GetSearchURL() + searchQuery.Replace("-", "+"));

            var foundSongs = pageContent.Explode("<a class=\"dlink\" href=\"");

            var loopIndex = base.GetBaseIndex();

            while (true)
            {
                if (loopIndex >= foundSongs.Length)
                    break;

                var currentSongURL = base.GetBaseURL() + foundSongs[loopIndex].Explode("\">Download</a>")[0];

                var currentSongPageContent = new DroppableWebClient().DownloadStringDispose(currentSongURL);

                var downloadURL = string.Empty;

                if (currentSongPageContent.Contains("<audio autoplay=\"\" controls=\"\" width=\"120px\" height=\"30px\"><source src=\"")
                    && currentSongPageContent.Contains("\" type"))
                {
                    downloadURL = base.GetBaseURL()
                        + currentSongPageContent.Explode("<audio autoplay=\"\" controls=\"\" width=\"120px\" height=\"30px\"><source src=\"")[1].Explode("\" type")[0];
                }
                else
                {
                    downloadURL = base.GetBaseURL() + "/" + currentSongPageContent.Explode("\" class=\"glink\">Free Download</a> - <a href=\"")[1].Explode("\" class=")[0];
                }

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
