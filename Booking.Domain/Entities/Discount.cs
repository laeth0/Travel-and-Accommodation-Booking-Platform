


namespace Booking.Domain.Entities;
public class Discount : BaseEntity, IAuditableEntity
{
    public int Percentage { get; set; }

    public string Description { get; set; }

    public DateTime StartDateUtc { get; set; }

    public DateTime EndDateUtc { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? ModifiedAtUtc { get; set; }

    public Guid RoomId { get; set; }
    public virtual Room Room { get; set; }
}
