using System;
using System.Collections.Generic;
using System.Text;

namespace IngaiaInterview.Domain.Music
{
    public class Genre
    {

        public Genre(string name, string playlistId)
        {
            Name = name;
            PlaylistId = playlistId;
        }
        public string Name { get; }
        public string PlaylistId { get; }
    }
}
