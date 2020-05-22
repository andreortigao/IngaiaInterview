using IngaiaInterview.Domain.Music;
using IngaiaInterview.Infrastructure;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngaiaInterview.Repository.Music
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly EnvironmentSettings _environmentSettings;

        public PlaylistRepository(EnvironmentSettings environmentSettings)
        {
            _environmentSettings = environmentSettings;
        }

        public async Task<Playlist> GetPlaylistByGenre(Genre genre)
        {
            CredentialsAuth auth = new CredentialsAuth(_environmentSettings.SpotifyClientId, _environmentSettings.SpotifyClientSecret);
            Token token = await auth.GetToken();
            var spotify = new SpotifyWebAPI()
            {
                AccessToken = token.AccessToken,
                TokenType = token.TokenType
            };

            Paging<PlaylistTrack> playlist = spotify.GetPlaylistTracks(genre.PlaylistId);
            var trackNames = playlist.Items.Select(x => x.Track.Name).ToList();

            return new Playlist(trackNames);
        }
    }
}
