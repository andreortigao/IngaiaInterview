using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IngaiaInterview.Application.Statistics.Queries
{
    public class GetCitySearchStatisticsQuery : IRequest<IList<CitySearchStatisticsModel>>
    {
    }
}
