

namespace TravelAccommodationBookingPlatform.Domain.Entities;
public class Country : BaseEntity
{

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid? ImageId { get; set; }

    public virtual Image? Image { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}



