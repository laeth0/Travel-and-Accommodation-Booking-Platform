namespace TravelAccommodationBookingPlatform.Domain.Entities;

public class Token : BaseEntity
{
    public string Value { get; set; }

    public bool IsRevoked { get;  set; } = false;

    public string AppUserId { get; set; }

    public virtual AppUser AppUser { get; set; }

    public DateTime ExpiresAt { get; set; }

}
