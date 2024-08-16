




namespace Booking.Application.Mediatr;
public class ResidenceResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string ImagePublicId { get; set; }
    public string ImageUrl { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int FloorsNumber { get; set; }
    public float Rating { get; set; }

    public Guid CityId { get; set; }
    public string City { get; set; }

    public Guid ResidenceTypeId { get; set; }
    public string ResidenceType { get; set; }
}
