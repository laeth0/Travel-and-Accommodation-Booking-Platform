

namespace TravelAccommodationBookingPlatform.Domain.Entities;
public class City : BaseEntity
{

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid CountryId { get; set; }

    public virtual Country Country { get; set; }

    public Guid? ImageId { get; set; }

    public virtual Image? Image { get; set; }

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();


}
