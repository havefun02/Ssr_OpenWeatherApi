namespace App.Models
{
    public class WeatherResult
    {
        public string? CityName { get; set; }
        public string? CountryName { get; set; }
        public Coord? Coord { get; set; }
        public double Temp { get; set; }
        public double Humidity { get; set; }
        public int Timezone { get; set; }
        public double WindSpeed { get; set; } // Add Wind Speed
        public int Cloudiness { get; set; }   // Add Cloudiness
    }
}
