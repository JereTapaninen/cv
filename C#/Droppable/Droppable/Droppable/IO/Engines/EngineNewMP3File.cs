using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using Droppable.IO.Utils;

namespace Droppable.IO.Engines
{
    public class EngineNewMP3File : Engine
    {
        public EngineNewMP3File() :
            base("NewMP3File", "http://song.newmp3file.com/", "search/", 1)
        { }

        public override string Search(SongInfo info)
        {
            var pageContent = new DroppableWebClient().DownloadStringDispose(base.GetBaseURL() + base.GetSearchURL() + info.ArtistName.Replace("+", "-") + "-" + info.SongName.Replace("+", "-"));

            var foundSongs = pageContent.Explode("<div class=\"fl odd\"><a href=\"");

            var loopIndex = base.GetBaseIndex();

            while (true)
            {
                if (loopIndex >= foundSongs.Length)
                    break;

                var currentSongURL = base.GetBaseURL() + foundSongs[loopIndex].Explode("\" title=\"")[0];

                var currentSongPageContent = new DroppableWebClient().DownloadStringDispose(currentSongURL);

                var downloadURL = base.GetBaseURL() + currentSongPageContent.Explode("<br/>Download : <a class=\"dwnLink\" href=\"")[1].Explode("\" rel")[0];

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
            var pageContent = new DroppableWebClient().DownloadStringDispose(base.GetBaseURL() + base.GetSearchURL() + searchQuery.Replace("+", "-"));

            var foundSongs = pageContent.Explode("<div class=\"fl odd\"><a href=\"");

            var loopIndex = base.GetBaseIndex();

            while (true)
            {
                if (loopIndex >= foundSongs.Length)
                    break;

                var currentSongURL = base.GetBaseURL() + foundSongs[loopIndex].Explode("\" title=\"")[0];

                var currentSongPageContent = new DroppableWebClient().DownloadStringDispose(currentSongURL);

                var downloadURL = base.GetBaseURL() + currentSongPageContent.Explode("<br/>Download : <a class=\"dwnLink\" href=\"")[1].Explode("\" rel")[0];

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