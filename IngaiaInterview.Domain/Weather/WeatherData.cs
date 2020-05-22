using System;
using System.Collections.Generic;
using System.Text;

namespace IngaiaInterview.Domain.Weather
{
    public class WeatherData
    {
        public WeatherData(string cityName, decimal temperature)
        {
            CityName = cityName;
            Temperature = temperature;
        }

        public string CityName { get; }
        public decimal Temperature { get;  }
    }
}
