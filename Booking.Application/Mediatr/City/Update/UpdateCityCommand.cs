

using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Mediatr;
public class UpdateCityCommand : IRequest<CityResponse>
{

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }
    public Guid CountryId { get; set; }
}