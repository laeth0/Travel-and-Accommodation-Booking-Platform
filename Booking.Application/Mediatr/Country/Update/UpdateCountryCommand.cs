

using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Mediatr;
public class UpdateCountryCommand : IRequest<CountryResponse>
{

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }
}