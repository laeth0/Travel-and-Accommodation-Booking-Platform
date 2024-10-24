namespace TravelAccommodationBookingPlatform.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public string Value { get; set; }

    public bool IsRevoked { get; set; } = false;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAy { get; set; } = DateTime.UtcNow;

    public string AppUserId { get; set; }

    public virtual AppUser AppUser { get; set; }

    public Guid JwtId { get; set; }



}
