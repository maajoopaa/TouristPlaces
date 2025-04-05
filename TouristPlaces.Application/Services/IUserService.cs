using Microsoft.AspNetCore.Http;
using TouristPlaces.DataAccess.Entities;

namespace TouristPlaces.Application.Services
{
    public interface IUserService
    {
        Task<bool> Authorize(UserEntity user, HttpContext context);
        Task<UserEntity?> Create(string username, string password);
        Task<UserEntity?> Get(Guid id);
        Task<UserEntity?> Get(string username, string password);
    }
}