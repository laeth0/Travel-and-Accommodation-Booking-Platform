



namespace Booking.Domain.Entities;
public class Amenity : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public virtual ICollection<RoomAmenity> RoomAmenities { get; set; } = new HashSet<RoomAmenity>();
}
