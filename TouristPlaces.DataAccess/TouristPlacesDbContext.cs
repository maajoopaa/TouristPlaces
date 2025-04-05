using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristPlaces.DataAccess.Configurations;
using TouristPlaces.DataAccess.Entities;

namespace TouristPlaces.DataAccess
{
    public class TouristPlacesDbContext : DbContext
    {
        public TouristPlacesDbContext(DbContextOptions<TouristPlacesDbContext> options)
            : base(options) { }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<PlaceEntity> Places { get; set; }

        public DbSet<RegionEntity> Regions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PlaceConfiguration());
            modelBuilder.ApplyConfiguration(new RegionConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
