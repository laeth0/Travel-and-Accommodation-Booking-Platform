



namespace Booking.Domain.Entities;
public class RoomBooking : BaseEntity, IAuditableEntity
{

    public int TotalPrice { get; set; }
    public DateTime CheckInDateUtc { get; set; }
    public DateTime CheckOutDateUtc { get; set; }

    public string UserRemarks { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? ModifiedAtUtc { get; set; }
    public int? Rating { get; set; }

    public Guid RoomId { get; set; }
    public virtual Room Room { get; set; }

    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }

}
