using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristPlaces.DataAccess;
using TouristPlaces.DataAccess.Entities;

namespace TouristPlaces.Application.Services
{
    public class RegionService : IRegionService
    {
        private readonly TouristPlacesDbContext _context;

        public RegionService(TouristPlacesDbContext context)
        {
            _context = context;
        }

        public async Task<List<RegionEntity>> GetList()
        {
            try
            {
                var regionEntities = await _context.Regions
                    .AsNoTracking()
                    .ToListAsync();

                return regionEntities;
            }
            catch
            {
                return new List<RegionEntity>();
            }
        }
    }
}
