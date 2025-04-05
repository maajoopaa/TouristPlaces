using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TouristPlaces.Application.Services;
using TouristPlaces.DataAccess.Entities;
using TouristPlaces.Models;

namespace TouristPlaces.Controllers
{
    public class PlaceController : Controller
    {
        private readonly IPlaceService _placeService;
        private readonly IUserService _userService;
        private readonly IRegionService _regionService;

        public PlaceController(IPlaceService placeService, IUserService userService, IRegionService regionService)
        {
            _placeService = placeService;
            _userService = userService;
            _regionService = regionService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPlace(string id)
        {
            var place = await _placeService.Get(Guid.Parse(id));

            var regions = await _regionService.GetList();

            ViewData["Regions"] = regions;

            return View(place);
        }

        [HttpGet]
        public async Task<ActionResult> CreatePlace()
        {
            var regions = await _regionService.GetList();

            return PartialView(regions);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePlace([FromBody] PlaceRequest model)
        {
            var user = await GetUser();

            if (user != null && user.IsAdmin == 1)
            {
                var place = await _placeService.Create(model.Title, model.Description, Convert.FromBase64String(model.Image), Guid.Parse(model.RegionId));

                if (place != null)
                {
                    return StatusCode(200);
                }

                return StatusCode(404);
            }

            return StatusCode(403);
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePlace(string id, [FromBody] PlaceRequest model)
        {
            var user = await GetUser();

            if (user != null && user.IsAdmin == 1)
            {
                var place = await _placeService.Update(Guid.Parse(id), model.Title, model.Description, Convert.FromBase64String(model.Image), Guid.Parse(model.RegionId));

                if (place != null)
                {
                    return StatusCode(200);
                }

                return StatusCode(404);
            }

            return StatusCode(403);
        }

        [HttpDelete]
        public async Task<ActionResult> RemovePlace(string id)
        {
            var user = await GetUser();

            if (user != null && user.IsAdmin == 1)
            {
                var isSuccess = await _placeService.Remove(Guid.Parse(id));

                if (isSuccess)
                {
                    return StatusCode(200);
                }

                return StatusCode(404);
            }

            return StatusCode(403);
        }

        private async Task<UserEntity?> GetUser()
        {
            var claimValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (claimValue is not null)
            {
                var user = await _userService.Get(Guid.Parse(claimValue));

                return user;
            }

            return null;
        }
    }
}
