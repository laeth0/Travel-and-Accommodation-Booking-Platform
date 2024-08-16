using AutoMapper;
using Booking.API.DTOs;
using Booking.Application.Mediatr;
using Booking.Domain.Entities;

namespace Booking.API.Mapping;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UserResponse>();

        CreateMap<UpdateProfileRequest, UpdateUserProfileCommand>();
        CreateMap<UpdateUserProfileCommand, ApplicationUser>();
    }
}
