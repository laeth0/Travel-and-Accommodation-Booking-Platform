


namespace Booking.Domain.Entities;
public class ResidenceOwner : BaseEntity, IAuditableEntity
{
    public DateOnly PurchaseDate { get; set; }
    public DateOnly? SaleDate { get; set; }

    public DateTime CreatedAtUtc { get ; set ; }
    public DateTime? ModifiedAtUtc { get ; set ; }

    public float OwnershipPercentage { get; set; }

    public Guid ResidenceId { get; set; }
    public virtual Residence Residence { get; set; }

    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }


}
