using MediatR;

namespace IngaiaInterview.Application.Playlist.Queries.GetPlaylistForCity
{
    public class GetPlaylistForCityQuery : IRequest<CityPlaylistModel>
    {
        public string CityName { get; }

        public GetPlaylistForCityQuery(string cityName) => CityName = cityName;
    }
}
