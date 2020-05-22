using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IngaiaInterview.Domain.Music
{
    public interface IPlaylistRepository
    {
        public Task<Playlist> GetPlaylistByGenre(Genre genre);
    }
}
