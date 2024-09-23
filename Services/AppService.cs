using App.AppInterfaces;
using App.Models;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text.Json;

namespace App.Services
{
    public class AppService:IAppService
    {
        private readonly HttpClient _httpClient;

        public AppService(HttpClient httpClient) { 
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://api.openweathermap.org/"); 
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        public async Task<Location> GetLocationAsync(string cityName,string apiKey,string endpoint)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var res=await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<Location>>(res);
                if (data != null)
                    return data[0];
                else throw new Exception("Cannot find the city");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }

        public async Task<WeatherResponse> GetDataByCityAsync(string cityName)
        {
            string apiKey = "1ef6f42f9c77c077c8bbf226d75d887e";
            string locationEndpoint = $"geo/1.0/direct?q={cityName}&limit=1&appid={apiKey}"; // Correctly interpolate variables
            var location =await this.GetLocationAsync(cityName, apiKey, locationEndpoint);
            string weatherDataEndpoint = $"data/2.5/weather?lat={location.Lat}&lon={location.Lon}&appid={apiKey}"; // Correctly interpolate variables
            HttpResponseMessage response = await _httpClient.GetAsync(weatherDataEndpoint);
            if (response.IsSuccessStatusCode)
            {
                var res= await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<WeatherResponse>(res);
                if (data != null)
                    return data;
                else throw new Exception("Cannot find data");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }
        public async Task<WeatherResponse> GetDataByLocationAsync(double lat,double lon)
        {
            string apiKey = "1ef6f42f9c77c077c8bbf226d75d887e";
            string weatherDataEndpoint = $"data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}"; // Correctly interpolate variables
            HttpResponseMessage response = await _httpClient.GetAsync(weatherDataEndpoint);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<WeatherResponse>(res);
                if (data != null)
                    return data;
                else throw new Exception("Cannot find data");
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }

        }


    }
}
