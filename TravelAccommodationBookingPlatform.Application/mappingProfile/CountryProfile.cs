using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Features.Country.Create;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.mappingProfile;
public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<CountryCreateCommand, Country>();
        CreateMap<Country, CountryResponse>()
            .ForMember(dest => dest.Image,
            opt => opt.MapFrom(src => src.Image != null ? src.Image.Url : null));
    }

}
