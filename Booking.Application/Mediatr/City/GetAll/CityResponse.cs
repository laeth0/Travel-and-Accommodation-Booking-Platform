


namespace Booking.Application.Mediatr;
public class CityResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImagePublicId { get; set; }
    public string ImageUrl { get; set; }

    public Guid CountryId { get; set; }
    public string Country { get; set; }

}