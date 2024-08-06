namespace Booking.PL.MappingProfiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {

            CreateMap<Room, RoomResponseDTO>()
                       .ForMember(dest => dest.Residence,
opt => opt.MapFrom(src => src.Residence.Name));


            CreateMap<RoomCreateRequestDTO, Room>();
            CreateMap<RoomUpdateRequestDTO, Room>();
        }
    }
}
