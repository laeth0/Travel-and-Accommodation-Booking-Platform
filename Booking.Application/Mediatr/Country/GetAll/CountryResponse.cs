

namespace Booking.Application.Mediatr;
public class CountryResponse
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImagePublicId { get; set; }
    public string ImageUrl { get; set; }

}