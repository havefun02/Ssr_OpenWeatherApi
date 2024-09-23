using App.AppInterfaces;
using App.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WeatherThirdParty.Controllers
{
    public class AppController:Controller
    {
        private readonly IAppService _appService;
        private readonly IMapper _mapper;

        public AppController(IAppService appService,IMapper mapper) {
            _appService = appService;
            _mapper = mapper;
        } 
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SearchCity([FromBody] string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                return BadRequest("City cannot be empty.");
            }
            var data = await _appService.GetDataByCityAsync(city);
            var res = _mapper.Map<WeatherResult>(data);
            return PartialView("_CityData", res);
        }
        [HttpPost]
        public async Task<IActionResult> UserLocation([FromBody] UserLocation userLocation)
        {
            if (!ModelState.IsValid) {
                return BadRequest("Invalid location.");
            }
            var data = await _appService.GetDataByLocationAsync(userLocation.lat,userLocation.lon);
            var res = _mapper.Map<WeatherResult>(data);

            return PartialView("_UserLocationData",res);

        }

    }
}
