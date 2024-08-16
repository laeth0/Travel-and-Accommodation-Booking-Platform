

using AutoMapper;
using Booking.API.DTOs;
using Booking.Application.Mediatr;
using Booking.Domain.Entities;

namespace Booking.API.Mapping;
public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<CountryCreateRequest, CreateCountryCommand>();
        CreateMap<CountryCreateRequest, UpdateCountryCommand>();

        CreateMap<UpdateCountryCommand, Country>()
            .ForMember(x => x.ImagePublicId,
            opt => opt.Ignore())
            .ForMember(x => x.ImageUrl,
            opt => opt.Ignore());


        CreateMap<CreateCountryCommand, Country>();
        CreateMap<Country, CountryResponse>();


    }
}
