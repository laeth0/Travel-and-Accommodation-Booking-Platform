using AutoMapper;
using Booking.API.DTOs;
using Booking.Application.Mediatr;
using Booking.Domain.Entities;

namespace Booking.API.Mapping;
public class RoomBookingProfile : Profile
{
    public RoomBookingProfile()
    {
        CreateMap<RoomBooking, RoomBookingResponse>();
        CreateMap<MakeBookingCommand, RoomBooking>();
        CreateMap<RoomBookingCreateRequest, MakeBookingCommand>();
    }
}
