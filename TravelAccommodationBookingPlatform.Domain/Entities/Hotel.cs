using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Domain.Entities;
public class Hotel : BaseEntity
{

    public string Name { get; set; }

    public string Description { get; set; }

    public Coordinates Coordinates { get; set; }

    public StarRate StarRate { get; set; }

    public Guid CityId { get; set; }

    public virtual City City { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

}
