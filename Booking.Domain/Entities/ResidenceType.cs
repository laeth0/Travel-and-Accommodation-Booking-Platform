


namespace Booking.Domain.Entities;
public class ResidenceType : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public virtual ICollection<Residence> Residences { get; set; } = new HashSet<Residence>();
}
