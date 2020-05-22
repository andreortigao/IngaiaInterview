using IngaiaInterview.Domain.Weather;
using IngaiaInterview.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IngaiaInterview.Repository.Weather
{
    public class WeatherDataRepository : IWeatherDataRepository
    {
        private readonly HttpClient _httpClient;
        private readonly EnvironmentSettings _settings;

        public WeatherDataRepository(HttpClient httpClient, EnvironmentSettings settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }

        public async Task<WeatherData> GetWeatherForCity(string cityName)
        {
            var url = $"http://api.openweathermap.org/data/2.5/weather?q={cityName},br&units=metric&APPID={_settings.OpenWeatherApiKey}";

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var jsonObject = JObject.Parse(await response.Content.ReadAsStringAsync());

            var temperature = jsonObject.SelectToken("$.main.temp", errorWhenNoMatch: true).Value<decimal>();

            return new WeatherData(cityName, temperature);
        }
    }
}
