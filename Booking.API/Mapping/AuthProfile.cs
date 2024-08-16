

using AutoMapper;
using Booking.Application.Mediatr;
using Booking.API.DTOs;
using Booking.Domain.Entities;


namespace Booking.Application.Mapping;
public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<LoginRequest, LoginCommand>();
        CreateMap<RegisterRequest, RegisterCommand>();
        CreateMap<RegisterCommand, ApplicationUser>();

    }

}