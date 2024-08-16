


namespace Booking.Infrastrature.Services;
public class JwtAuthConfig
{
    public required string Key { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required int DurationInDays { get; set; }
}

