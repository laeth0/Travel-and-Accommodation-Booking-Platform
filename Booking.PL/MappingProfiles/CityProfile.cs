using AutoMapper;
using Booking.DAL.Entities;
using Booking.PL.DTO.City;

namespace Booking.PL.MappingProfiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityResponseDTO>();

            CreateMap<CityCreateRequestDTO, City>();

            CreateMap<CityUpdateRequestDTO, City>();
        }
    }
}
