


namespace Booking.Domain.Entities;
public class ResidenceBooking : BaseEntity, IAuditableEntity
{
    public decimal TotalPrice { get; set; }

    public DateOnly CheckInDateUtc { get; set; }
    public DateOnly CheckOutDateUtc { get; set; }

    public string GuestRemarks { get; set; }

    public DateTime CreatedAtUtc { get; set; }
    public DateTime? ModifiedAtUtc { get; set; }

    public Guid ResidenceId { get; set; }
    public virtual Residence Residence { get; set; }

    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }

}
