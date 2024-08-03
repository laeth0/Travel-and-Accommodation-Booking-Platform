

namespace Booking.DAL.Entities;
public class Review
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Guid GuestRoomId { get; set; }
    public virtual GuestRoom GuestRoom { get; set; } = null!;


    public string UserId { get; set; } = null!;

    public virtual ApplicationUser User { get; set; } = null!;
}
