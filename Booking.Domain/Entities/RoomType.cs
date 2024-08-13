



namespace Booking.Domain.Entities;
public class RoomType : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new HashSet<Room>();

}
