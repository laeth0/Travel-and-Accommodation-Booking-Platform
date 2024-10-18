

namespace TravelAccommodationBookingPlatform.Domain.Entities;
public class Review : BaseEntity, IAuditableEntity
{


    public string Content { get; set; }

    public int Rating { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string AppUserId { get; set; }

    public virtual AppUser AppUser { get; set; }

    public Guid HotelId { get; set; }

    public virtual Hotel Hotel { get; set; }


}
