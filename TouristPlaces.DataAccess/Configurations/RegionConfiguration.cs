using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristPlaces.DataAccess.Entities;

namespace TouristPlaces.DataAccess.Configurations
{
    public class RegionConfiguration : IEntityTypeConfiguration<RegionEntity>
    {
        public void Configure(EntityTypeBuilder<RegionEntity> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasMany(r => r.Places)
                .WithOne(p => p.Region)
                .HasForeignKey(p => p.RegionId);
        }
    }
}
