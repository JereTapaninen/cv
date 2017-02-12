using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Droppable.IO
{
    public struct SongInfo
    {
        public string ArtistName;
        public string SongName;
        public string AlbumName;
        public string AlbumCoverURL;

        public SongInfo(string artist, string song)
        {
            this.ArtistName = artist;
            this.SongName = song;
            this.AlbumName = "";
            this.AlbumCoverURL = "";
        }

        public SongInfo(string artist, string song, string album, string cover)
        {
            this.ArtistName = artist;
            this.SongName = song;
            this.AlbumName = album;
            this.AlbumCoverURL = cover;
        }

        public override string ToString()
        {
            return this.ArtistName + "-" + this.SongName;
        }
    }
}
