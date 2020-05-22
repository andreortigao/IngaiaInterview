using IngaiaInterview.Domain.Weather;
using System;
using System.Collections.Generic;
using System.Text;

namespace IngaiaInterview.Domain.Music
{
    public class GenreService
    {
        public Genre GetRecommendedGenreByWeather(WeatherData weather)
        {
            return weather.Temperature switch
            {
                decimal t when t > 25 => new Genre("Pop", "37i9dQZF1DX6aTaZa0K6VA"),
                decimal t when t > 10 => new Genre("Rock", "37i9dQZF1DWXRqgorJj26U"),
                _ => new Genre("Classical", "1h0CEZCm6IbFTbxThn6Xcs"),
            };
        }
    }
}
