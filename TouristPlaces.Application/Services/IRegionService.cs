using TouristPlaces.DataAccess.Entities;

namespace TouristPlaces.Application.Services
{
    public interface IRegionService
    {
        Task<List<RegionEntity>> GetList();
    }
}