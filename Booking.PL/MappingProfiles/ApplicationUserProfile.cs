using AutoMapper;
using Booking.DAL.Entities;
using Booking.PL.DTO.Account;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Presentation.MappingProfiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<RegisterRequestDTO, ApplicationUser>()
                .ForMember(dest => dest.UserName,
                opt =>
                opt.MapFrom(src => src.FirstName + src.LastName));


            CreateMap<ApplicationUser, RegisterResponseDTO>();

            CreateMap<ApplicationUser, UserResponseDTO>();

            CreateMap<IdentityRole, RolesResponseDTO>();



        }
    }
}
