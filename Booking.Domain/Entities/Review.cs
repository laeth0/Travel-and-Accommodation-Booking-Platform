

namespace Booking.Domain.Entities;
public class Review : BaseEntity, IAuditableEntity
{
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? ModifiedAtUtc { get; set; }

    public Guid ResidenceId { get; set; }
    public virtual Residence Residence { get; set; }

    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }


}
