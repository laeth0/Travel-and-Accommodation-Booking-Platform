using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations.Associations;
public class RoomImageAssociation : BaseEntity
{

    public Guid ImageId { get; set; }
    public virtual Image Image { get; set; }

    public Guid RoomId { get; set; }
}
