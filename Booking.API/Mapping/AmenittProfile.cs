using AutoMapper;
using Booking.Application.Mediatr;
using Booking.Domain.Entities;

namespace Booking.API.Mapping;
public class AmenittProfile : Profile
{

    public AmenittProfile()
    {
        CreateMap<Amenity, AmenityResponse>();
    }
}
