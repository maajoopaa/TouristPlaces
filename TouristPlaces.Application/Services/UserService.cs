using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TouristPlaces.DataAccess;
using TouristPlaces.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace TouristPlaces.Application.Services
{
    public class UserService : IUserService
    {
        private readonly TouristPlacesDbContext _context;

        public UserService(TouristPlacesDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity?> Get(Guid id)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            return userEntity;
        }

        public async Task<UserEntity?> Get(string username, string password)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            return userEntity;
        }

        public async Task<UserEntity?> Create(string username, string password)
        {
            try
            {
                var userEntity = new UserEntity
                {
                    Username = username,
                    Password = password
                };

                await _context.Users
                    .AddAsync(userEntity);

                await _context.SaveChangesAsync();

                return userEntity;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Authorize(UserEntity user, HttpContext context)
        {
            try
            {
                var claims = new List<Claim> {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Role, user.IsAdmin == 1 ? "Admin" : "User")
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.NameIdentifier,
                    ClaimTypes.Role
                );

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await context.SignInAsync(claimsPrincipal);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
