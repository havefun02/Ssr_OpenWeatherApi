using App.Models;
using AutoMapper;
namespace App.Core

{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<WeatherResponse, WeatherResult>()
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Sys!.Country))
            .ForMember(dest => dest.Temp, opt => opt.MapFrom(src => src.Main!.Temp))
            .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Main!.Humidity))
            .ForMember(dest => dest.Timezone, opt => opt.MapFrom(src => src.Timezone))
            .ForMember(dest => dest.WindSpeed, opt => opt.MapFrom(src => src.Wind!.Speed)) // Map Wind Speed
            .ForMember(dest => dest.Cloudiness, opt => opt.MapFrom(src => src.Clouds!.All)); // Map Cloudiness

        }
    }
}
