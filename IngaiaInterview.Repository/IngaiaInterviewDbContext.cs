using IngaiaInterview.Domain.Statistics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IngaiaInterview.Repository
{
    public class IngaiaInterviewDbContext : DbContext
    {
        public IngaiaInterviewDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IngaiaInterviewDbContext).Assembly);
        }

        public DbSet<CityStatistic> CityStatistics { get; protected set; }
    }
}
