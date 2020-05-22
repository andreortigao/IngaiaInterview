using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IngaiaInterview.Domain.Weather
{
    public interface IWeatherDataRepository
    {
        public Task<WeatherData> GetWeatherForCity(string cityName);
    }
}
