
using Booking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Mediatr;
public class CreateCityCommand : IRequest<CityResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }

    public Guid CountryId { get; set; }
}