using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TouristPlaces.Application.Services;
using TouristPlaces.Models;

namespace TouristPlaces.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRegionService _regionService;
        private readonly IPlaceService _placeService;

        public HomeController(ILogger<HomeController> logger, IRegionService regionService, IPlaceService placeService)
        {
            _logger = logger;
            _regionService = regionService;
            _placeService = placeService;
        }

        public async Task<ActionResult> Index()
        {
            var regions = await _regionService.GetList();

            return View(regions);
        }

        public async Task<ActionResult> Search(string query)
        {
            var places = await _placeService.Search(query);

            return View(places);
        }

        public async Task<ActionResult> Sort(string regionId)
        {
            var places = await _placeService.Sort(regionId);
            
            return View(places);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
