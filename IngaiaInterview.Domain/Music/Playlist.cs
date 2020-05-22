using System;
using System.Collections.Generic;
using System.Text;

namespace IngaiaInterview.Domain.Music
{
    public class Playlist
    {
        public Playlist(IList<string> songNames)
        {
            SongNames = songNames;
        }

        public IList<string> SongNames { get; }
    }
}
