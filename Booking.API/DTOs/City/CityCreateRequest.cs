namespace Booking.API.DTOs.City;
public class CityCreateRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }

    public Guid CountryId { get; set; }
}
