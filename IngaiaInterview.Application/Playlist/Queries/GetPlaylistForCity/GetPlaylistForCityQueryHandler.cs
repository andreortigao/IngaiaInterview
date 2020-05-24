using IngaiaInterview.Application.Exceptions;
using IngaiaInterview.Domain.Music;
using IngaiaInterview.Domain.Statistics;
using IngaiaInterview.Domain.Weather;
using IngaiaInterview.Repository;
using MediatR;
using NodaTime;
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
        private readonly IngaiaInterviewDbContext _dbContext;
        private readonly IClock _clock;

        public GetPlaylistForCityQueryHandler(IWeatherDataRepository weatherRepository, IPlaylistRepository playlistRepository, IngaiaInterviewDbContext dbContext, IClock clock)
        {
            _weatherRepository = weatherRepository;
            _playlistRepository = playlistRepository;
            _dbContext = dbContext;
            _clock = clock;
        }

        public async Task<CityPlaylistModel> Handle(GetPlaylistForCityQuery request, CancellationToken cancellationToken)
        {
            var weatherData = await _weatherRepository.GetWeatherForCity(request.CityName);

            _dbContext.CityStatistics.Add(new CityStatistic(request.CityName, _clock));
            await _dbContext.SaveChangesAsync();

            var genre = new GenreService().GetRecommendedGenreByWeather(weatherData);

            var playlist = await _playlistRepository.GetPlaylistByGenre(genre);

            return new CityPlaylistModel(request.CityName, weatherData.Temperature, genre.Name, playlist.SongNames);
        }
    }
}
