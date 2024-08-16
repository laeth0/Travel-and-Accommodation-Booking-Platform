

using AutoMapper;
using Booking.Application.Mediatr;
using Booking.Domain.Entities;

namespace Booking.API.Mapping;

public class ResidenceTypeProfile : Profile
{
    public ResidenceTypeProfile()
    {
        CreateMap<ResidenceType, ResidenceTypeResponse>();
    }
}
