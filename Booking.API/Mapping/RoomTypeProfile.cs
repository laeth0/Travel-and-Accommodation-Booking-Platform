using AutoMapper;
using Booking.Application.Mediatr;
using Booking.Domain.Entities;

namespace Booking.API.Mapping;
public class RoomTypeProfile : Profile
{
    public RoomTypeProfile()
    {
        CreateMap<RoomType, RoomTypeResponse>();
    }
}
