


namespace Booking.Application.Mediatr;
public class CityResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageName { get; set; }

    public Guid CountryId { get; set; }
    public string Country { get; set; }

}