using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace IngaiaInterview.Domain.Statistics
{
    public class CityStatistic
    {
        protected CityStatistic()
        {

        }

        public CityStatistic(string cityName, IClock clock)
        {
            CityName = cityName;
            TimeStamp = clock.GetCurrentInstant().ToDateTimeUtc();
        }

        public int Id { get; protected set; }
        public string CityName { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
