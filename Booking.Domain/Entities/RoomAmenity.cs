

namespace Booking.Domain.Entities;
public class RoomAmenity : BaseEntity
{
    public Guid RoomId { get; set; }
    public virtual Room Room { get; set; }

    public Guid AmenityId { get; set; }
    public virtual Amenity Amenity { get; set; }
}
