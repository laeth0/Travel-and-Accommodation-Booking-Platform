using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations.Associations;

public class HotelImageAssociation : BaseEntity
{

    public Guid ImageId { get; set; }
    public virtual Image Image { get; set; }

    public Guid HotelId { get; set; }
}