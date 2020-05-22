using System;
using System.Collections.Generic;
using System.Text;

namespace IngaiaInterview.Application.Playlist.Queries.GetPlaylistForCity
{
    public class CityPlaylistModel
    {
        public CityPlaylistModel(string name, decimal temperature, string genre, IList<string> songs)
        {
            Name = name;
            Temperature = temperature;
            Genre = genre;
            Songs = songs;
        }

        public string Name { get; }
        public decimal Temperature { get; }
        public string Genre { get; set; }
        public IList<string> Songs { get; }
    }
}
