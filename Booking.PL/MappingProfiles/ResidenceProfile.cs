




namespace Booking.PL.MappingProfiles;
public class ResidenceProfile : Profile
{
    public ResidenceProfile()
    {
        CreateMap<Residence,ResidenceResponseDTO>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner.UserName));


        CreateMap<ResidenceCreateRequestDTO, Residence>();

        CreateMap<ResidenceUpdateRequestDTO, Residence>();


    }
}
