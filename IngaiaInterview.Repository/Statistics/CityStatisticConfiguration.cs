using IngaiaInterview.Domain.Statistics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IngaiaInterview.Repository.Statistics
{
    public class CityStatisticConfiguration : IEntityTypeConfiguration<CityStatistic>
    {
        public void Configure(EntityTypeBuilder<CityStatistic> builder)
        {
            builder.ToTable("CityStatistic");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CityName).IsRequired();
            builder.Property(x => x.TimeStamp).HasColumnName("Timestamp");
        }
    }
}
