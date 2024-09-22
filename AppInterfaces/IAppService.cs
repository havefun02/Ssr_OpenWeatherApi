using App.Models;

namespace App.AppInterfaces
{
    public interface IAppService
    {
        public Task<WeatherResponse> GetDataAsync(string cityName);

        public Task<Location> GetLocationAsync(string cityName, string apiKey, string endpoint);


    }
}
