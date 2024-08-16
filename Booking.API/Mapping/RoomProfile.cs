using AutoMapper;
using Booking.API.DTOs;
using Booking.Application.Mediatr;
using Booking.Domain.Entities;

namespace Booking.API.Mapping;
public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Room, RoomResponse>()
            .ForMember(x => x.Residence,
opt => opt.MapFrom(x => x.Residence.Name))
            .ForMember(x => x.RoomType,
opt => opt.MapFrom(x => x.RoomType.Name));


        CreateMap<CreateRoomCommand, Room>();
        CreateMap<RoomCreateRequest, CreateRoomCommand>();


    }
}
