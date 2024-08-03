

namespace Booking.PL.DTO.Residence;
public class ResidenceUpdateRequestDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Address { get; set; } = null!;
    public IFormFile Image { get; set; } = null!;

    [Phone]
    public string PhoneNumber { get; set; } = null!;

    [EmailAddress]
    public string Email { get; set; } = null!;

    public int FloorsNumber { get; set; }
    public int Rating { get; set; }
    public Guid CityId { get; set; }
    public string OwnerId { get; set; } = null!;
}
