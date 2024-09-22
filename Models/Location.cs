using System.Text.Json.Serialization;

namespace App.Models
{
    public class Location
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lon")]
        public double Lon { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }
    }
}
