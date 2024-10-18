

namespace TravelAccommodationBookingPlatform.Domain.Entities;
public class Discount : BaseEntity
{

    public string Code { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Percentage { get; set; }

    public Guid RoomId { get; set; }

    public virtual Room Room { get; set; }
}
