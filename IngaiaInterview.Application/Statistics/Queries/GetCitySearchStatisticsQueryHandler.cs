using IngaiaInterview.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IngaiaInterview.Application.Statistics.Queries
{
    public class GetCitySearchStatisticsQueryHandler : IRequestHandler<GetCitySearchStatisticsQuery, IList<CitySearchStatisticsModel>>
    {
        private readonly IngaiaInterviewDbContext _dbContext;

        public GetCitySearchStatisticsQueryHandler(IngaiaInterviewDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<CitySearchStatisticsModel>> Handle(GetCitySearchStatisticsQuery request, CancellationToken cancellationToken)
        {
            return await (
                from cs in _dbContext.CityStatistics
                group cs by cs.CityName into g
                select new
                {
                    name = g.Key,
                    count = g.Count()
                } into x
                select new CitySearchStatisticsModel
                {
                    Name = x.name,
                    Count = x.count
                }).ToListAsync();
        }
    }
}
