
using AutoMapper;
using Booking.API.DTOs;
using Booking.Application.Mediatr;
using Booking.Domain.Entities;

namespace Booking.API.Mapping;

public class ResidenceProfile : Profile
{
    public ResidenceProfile()
    {
        CreateMap<Residence, ResidenceResponse>().ForMember(x => x.City,
opt => opt.MapFrom(x => x.City.Name))
            .ForMember(x => x.ResidenceType,
opt => opt.MapFrom(x => x.ResidenceType.Name));


        CreateMap<CreateResidenceCommand, Residence>();
        CreateMap<UpdateResidenceCommand, Residence>()
            .ForMember(x => x.ImagePublicId,
            opt => opt.Ignore())
            .ForMember(x => x.ImageUrl,
            opt => opt.Ignore());



        CreateMap<ResidenceCreateRequest, CreateResidenceCommand>();
        CreateMap<ResidenceCreateRequest, UpdateResidenceCommand>();
    }
}
