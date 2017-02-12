using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using Droppable.IO.Utils;

namespace Droppable.IO.Engines
{
    public class EngineBeeMP3s : Engine
    {
        public EngineBeeMP3s() : 
            base("BeeMP3s", "http://beemp3s.org/", "search?query=", 1)
        { }

        public override string Search(SongInfo info)
        {
            var pageContent = new DroppableWebClient().DownloadStringDispose(base.GetBaseURL() + base.GetSearchURL() + info.ToString());

            var foundSongs = pageContent.Explode("<span>Download song:</span>");

            var loopIndex = base.GetBaseIndex();

            while (true)
            {
                if (loopIndex >= foundSongs.Length)
                    break;

                var currentSongURL = foundSongs[loopIndex].Explode("<a href=\"")[1].Explode("\">")[0];

                var currentSongPageContent = new DroppableWebClient().DownloadStringDispose(currentSongURL);

                var downloadURL = currentSongPageContent.Explode("<a class=\"big-green-btn mgLockedElement\" id=\"download-button\" href=\"")[1].Explode("\" target=")[0];

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
            var pageContent = new DroppableWebClient().DownloadStringDispose(base.GetBaseURL() + base.GetSearchURL() + searchQuery);

            var foundSongs = pageContent.Explode("<span>Download song:</span>");

            var loopIndex = base.GetBaseIndex();

            while (true)
            {
                if (loopIndex >= foundSongs.Length)
                    break;

                var currentSongURL = foundSongs[loopIndex].Explode("<a href=\"")[1].Explode("\">")[0];

                var currentSongPageContent = new DroppableWebClient().DownloadStringDispose(currentSongURL);

                var downloadURL = currentSongPageContent.Explode("<a class=\"big-green-btn mgLockedElement\" id=\"download-button\" href=\"")[1].Explode("\" target=")[0];

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
