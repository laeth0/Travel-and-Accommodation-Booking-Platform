


namespace Booking.DAL.Entities;
public class UserFlight
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string UserId { get; set; } = null!;
    public virtual ApplicationUser User { get; set; } = null!;

    public Guid FlightId { get; set; }
    public virtual Flight Flight { get; set; } = null!;

}

