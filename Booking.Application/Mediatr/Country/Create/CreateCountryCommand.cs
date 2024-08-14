
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Mediatr;
public class CreateCountryCommand : IRequest<CountryResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }
}