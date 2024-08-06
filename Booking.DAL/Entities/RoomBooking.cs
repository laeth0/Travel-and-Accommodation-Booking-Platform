
namespace Booking.DAL.Entities;

public class RoomBooking
{
    public Guid Id { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public int TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;


    public Guid RoomId { get; set; }
    public virtual Room Room { get; set; } = null!;

    public string GuestId { get; set; } = null!;
    public virtual ApplicationUser Guest { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();


}
