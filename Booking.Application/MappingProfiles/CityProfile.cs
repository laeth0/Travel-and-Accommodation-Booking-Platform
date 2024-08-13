



namespace Booking.PL.MappingProfiles;
public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<City, CityResponseDTO>()
            .ForMember(dest => dest.Country,
            opt => opt.MapFrom(src => src.Country.Name));


        CreateMap<CityCreateRequestDTO, City>();

    }
}
