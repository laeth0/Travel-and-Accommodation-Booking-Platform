
namespace Booking.DAL.Entities;

public class GuestRoom
{
    public Guid Id { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public int TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


    public Guid RoomId { get; set; }
    public Room Room { get; set; } = null!;

    public string GuestId { get; set; } = null!;
    public ApplicationUser Guest { get; set; } = null!;

    public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();


}
