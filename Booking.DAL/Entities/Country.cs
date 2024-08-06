



namespace Booking.DAL.Entities;
public class Country
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageName { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new HashSet<City>();
}
