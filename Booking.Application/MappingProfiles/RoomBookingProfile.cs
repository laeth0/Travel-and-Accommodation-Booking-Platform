


using Booking.PL.DTO.RoomBooking;

namespace Booking.PL.MappingProfiles;
public class RoomBookingProfile : Profile
{
    public RoomBookingProfile()
    {

        CreateMap<RoomBooking, RoomBookingResponseDTO>()
            .ForMember(dest => dest.Guest, opt => opt.MapFrom(src => src.Guest.UserName));


        CreateMap<RoomBookingCreateRequestDTO, RoomBooking>();

        CreateMap<RoomBookingUpdateRequestDTO, RoomBooking>();

    }
}
