using App.AppInterfaces;
using App.Models;
using AutoMapper;
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
            var data = await _appService.GetDataAsync(city);
            var res = _mapper.Map<WeatherResult>(data);
            return PartialView("_CityData", res);
        }

    }
}
