


using AutoMapper;
using Booking.API.DTOs;
using Booking.Application.Mediatr;
using Booking.Domain.Entities;

namespace Booking.API.Mapping;
public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<CityCreateRequest, CreateCityCommand>();
        CreateMap<CityCreateRequest, UpdateCityCommand>();

        CreateMap<UpdateCityCommand, City>()
            .ForMember(x => x.ImagePublicId,
            opt => opt.Ignore())
            .ForMember(x => x.ImageUrl,
            opt => opt.Ignore());


        CreateMap<CreateCityCommand, City>();

        CreateMap<City, CityResponse>().ForMember(x => x.Country,
            opt => opt.MapFrom(x => x.Country.Name));

    }
}
