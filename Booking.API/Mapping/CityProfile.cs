


using AutoMapper;
using Booking.API.DTOs.City;
using Booking.Application.Mediatr;
using Booking.Domain.Entities;

namespace Booking.API.Mapping;
public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<CityCreateRequest, CreateCityCommand>();
        CreateMap<CityCreateRequest, UpdateCityCommand>();
        CreateMap<UpdateCityCommand, City>().ForMember(x => x.ImageName,
            opt => opt.Ignore());
        CreateMap<CreateCityCommand, City>();
        CreateMap<City, CityResponse>().ForMember(x => x.Country,
            opt => opt.MapFrom(x => x.Country.Name));

    }
}
