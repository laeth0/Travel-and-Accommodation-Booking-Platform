



namespace Booking.Domain.Entities;
public class Country : BaseEntity
{
    public string Name { get; set; } 
    public string Description { get; set; } 
    public string ImageName { get; set; } 

    public virtual ICollection<City> Cities { get; set; } = new HashSet<City>();
}
