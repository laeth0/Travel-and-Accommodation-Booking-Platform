



namespace Booking.Domain.Entities;
public class City : BaseEntity
{
    public string Name { get; set; } 
    public string Description { get; set; }
    public string ImagePublicId { get; set; }
    public string ImageUrl { get; set; }


    public Guid CountryId { get; set; }
    public virtual Country Country { get; set; } 

    public virtual ICollection<Residence> Residences { get; set; } = new HashSet<Residence>();
}


