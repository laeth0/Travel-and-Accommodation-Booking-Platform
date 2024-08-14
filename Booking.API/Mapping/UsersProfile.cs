

using AutoMapper;
using Booking.Application.Mediatr.Users.Login;
using Booking.Application.Mediatr.Users.Register;
using Booking.API.DTOs;
using Booking.Domain.Entities;


namespace Booking.Application.Mapping;
public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<LoginRequest, LoginCommand>();
        CreateMap<RegisterRequest, RegisterCommand>();
        CreateMap<RegisterCommand, ApplicationUser>();
    }
}