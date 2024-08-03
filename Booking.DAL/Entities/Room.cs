

namespace Booking.DAL.Entities;

public class Room
{
    public Guid Id { get; set; }
    public int Capacity { get; set; }
    public int Price { get; set; }
    public float Rating { get; set; }
    public string Description { get; set; } = null!;
    public string ImageName { get; set; } = null!;

    public Guid ResidenceId { get; set; }
    public virtual Residence Residence { get; set; } = null!;


    public virtual ICollection<GuestRoom> GuestRooms { get; set; } = new HashSet<GuestRoom>();
}
