using App.Models;

namespace App.AppInterfaces
{
    public interface IAppService
    {
        public Task<WeatherResponse> GetDataByLocationAsync(double lat,double lon);
        public Task<WeatherResponse> GetDataByCityAsync(string cityName);

        public Task<Location> GetLocationAsync(string cityName, string apiKey, string endpoint);


    }
}
