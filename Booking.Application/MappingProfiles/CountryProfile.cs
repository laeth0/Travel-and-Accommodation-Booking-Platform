namespace Booking.PL.MappingProfiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryResponseDTO>();
            CreateMap<CountryCreateRequestDTO, Country>();
            CreateMap<CountryUpdateRequestDTO, Country>();
        }
    }

}
