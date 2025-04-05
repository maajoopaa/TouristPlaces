using TouristPlaces.DataAccess.Entities;

namespace TouristPlaces.Application.Services
{
    public interface IPlaceService
    {
        Task<PlaceEntity> Create(string title, string description, byte[] image, Guid regionId);
        Task<PlaceEntity?> Get(Guid id);
        Task<List<PlaceEntity>> GetList(Guid regionId);
        Task<bool> Remove(Guid id);
        Task<List<PlaceEntity>> Search(string query);
        Task<PlaceEntity?> Update(Guid id, string title, string description, byte[] image, Guid regionId);
        Task<List<PlaceEntity>> Sort(string regionId);
    }
}