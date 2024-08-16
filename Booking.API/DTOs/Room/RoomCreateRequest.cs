
namespace Booking.API.DTOs;
public class RoomCreateRequest
{
    public int AdultsCapacity { get; set; }
    public int ChildrenCapacity { get; set; }
    public int Number { get; set; }
    public int PricePerNight { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }
    public int Rating { get; set; }

    public Guid ResidenceId { get; set; }

    public Guid RoomTypeId { get; set; }
}
