namespace TravelAccommodationBookingPlatform.Domain.Models;
public class JwtAccessToken
{
    public Guid Id;

    public string Value;

    public DateTime ExpiryDate;

    public bool IsExpired => DateTime.UtcNow >= ExpiryDate;

    public DateTime CreatedDate = DateTime.UtcNow;


}
