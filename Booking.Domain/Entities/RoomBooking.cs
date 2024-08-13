



namespace Booking.Domain.Entities;
public class RoomBooking : BaseEntity
{

    public Guid RoomId { get; set; }
    public virtual Room Room { get; set; }

    public Guid ResidenceBookingId { get; set; }
    public virtual ResidenceBooking ResidenceBooking { get; set; }

}
