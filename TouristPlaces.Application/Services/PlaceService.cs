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
    public class PlaceService : IPlaceService
    {
        private readonly TouristPlacesDbContext _context;

        public PlaceService(TouristPlacesDbContext context)
        {
            _context = context;
        }

        public async Task<PlaceEntity?> Get(Guid id)
        {
            var placeEntity = await _context.Places
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            return placeEntity;
        }

        public async Task<List<PlaceEntity>> GetList(Guid regionId)
        {
            var placeEntities = await _context.Places
                .AsNoTracking()
                .Where(p => p.RegionId == regionId)
                .ToListAsync();

            return placeEntities;
        }

        public async Task<PlaceEntity> Create(string title, string description, byte[] image, Guid regionId)
        {
            var placeEntity = new PlaceEntity
            {
                Title = title,
                Description = description,
                Image = image,
                RegionId = regionId
            };

            await _context.Places
                .AddAsync(placeEntity);

            await _context.SaveChangesAsync();

            return placeEntity;
        }

        public async Task<PlaceEntity?> Update(Guid id, string title, string description, byte[] image, Guid regionId)
        {
            var placeEntity = await Get(id);

            if (placeEntity != null)
            {
                placeEntity.Title = title;
                placeEntity.Description = description;
                placeEntity.Image = image;
                placeEntity.RegionId = regionId;

                _context.Places
                    .Update(placeEntity);

                await _context.SaveChangesAsync();
            }

            return placeEntity;
        }

        public async Task<bool> Remove(Guid id)
        {
            try
            {
                var placeEntity = await Get(id);

                if (placeEntity != null)
                {
                    _context.Places
                        .Remove(placeEntity);

                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<PlaceEntity>> Search(string query)
        {
            var villageEntities = await _context.Places
                .AsNoTracking()
                .Where(p => p.Title.ToLower().Contains(query.ToLower()))
                .ToListAsync();

            return villageEntities;
        }

        public async Task<List<PlaceEntity>> Sort(string regionId)
        {
            var villageEntities = await _context.Places
                .AsNoTracking()
                .Where(p => p.RegionId == Guid.Parse(regionId))
                .ToListAsync();

            return villageEntities;
        }
    }
}
