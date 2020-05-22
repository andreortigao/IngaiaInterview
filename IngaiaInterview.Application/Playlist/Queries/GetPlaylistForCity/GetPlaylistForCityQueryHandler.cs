using IngaiaInterview.Application.Exceptions;
using IngaiaInterview.Domain.Music;
using IngaiaInterview.Domain.Weather;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IngaiaInterview.Application.Playlist.Queries.GetPlaylistForCity
{
    public class GetPlaylistForCityQueryHandler : IRequestHandler<GetPlaylistForCityQuery, CityPlaylistModel>
    {
        private readonly IWeatherDataRepository _weatherRepository;
        private readonly IPlaylistRepository _playlistRepository;

        public GetPlaylistForCityQueryHandler(IWeatherDataRepository weatherRepository, IPlaylistRepository playlistRepository)
        {
            _weatherRepository = weatherRepository;
            _playlistRepository = playlistRepository;
        }

        public async Task<CityPlaylistModel> Handle(GetPlaylistForCityQuery request, CancellationToken cancellationToken)
        {
            var weatherData = await _weatherRepository.GetWeatherForCity(request.CityName);

            var genre = new GenreService().GetRecommendedGenreByWeather(weatherData);

            var playlist = await _playlistRepository.GetPlaylistByGenre(genre);

            return new CityPlaylistModel(request.CityName, weatherData.Temperature, genre.Name, playlist.SongNames);
        }
    }
}
