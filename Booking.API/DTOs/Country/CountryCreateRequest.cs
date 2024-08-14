

namespace Booking.API.DTOs;
public class CountryCreateRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }
}
