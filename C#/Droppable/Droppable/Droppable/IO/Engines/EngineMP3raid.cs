using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using Droppable.IO.Utils;

namespace Droppable.IO.Engines
{
    public class EngineMP3raid : Engine
    {
        public EngineMP3raid() :
            base("MP3raid", "http://www.mp3raid2.com/", "download/", 1)
        { }

        public override string Search(SongInfo info)
        {
            var realSongInfo = info;
            realSongInfo.ArtistName = realSongInfo.ArtistName.Replace("+", " ");
            realSongInfo.SongName = realSongInfo.SongName.Replace("+", " ");

            var pageContent = new DroppableWebClient().DownloadStringDispose(base.GetBaseURL() + base.GetSearchURL() + realSongInfo.ToString() + ".html");

            var foundSongs = pageContent.Explode("<div class='index1'>");

            var loopIndex = base.GetBaseIndex();

            while (true)
            {
                if (loopIndex >= foundSongs.Length)
                    break;

                var currentSongId = foundSongs[loopIndex].Explode("<a href='javascript:;' class='dl' id='")[1].Explode("'>")[0];
                currentSongId = currentSongId.Replace("dl", "");

                var currentSongPageContent = new DroppableWebClient().DownloadStringDispose(base.GetBaseURL() + "search/ddl/" + currentSongId + "/" + realSongInfo.ArtistName + ".html");

                var downloadURL = currentSongPageContent.Explode("<b>Location:</b></td><td style='word-wrap:break-word;'>")[1].Explode("</td></tr>")[0];

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
            return null; // this engine doesn't support dirty search
        }
    }
}