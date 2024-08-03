

namespace Booking.DAL.Entities;

public class City
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageName { get; set; } = null!;
    public virtual ICollection<Residence> Residences { get; set; } = new HashSet<Residence>();
}
