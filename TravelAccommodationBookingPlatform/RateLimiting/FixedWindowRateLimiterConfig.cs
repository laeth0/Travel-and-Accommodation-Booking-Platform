using System.Threading.RateLimiting;

namespace TravelAccommodationBookingPlatform.Api.RateLimiting;

public class FixedWindowRateLimiterConfig
{
    public required int PermitLimit { get; set; }

    public required double TimeWindowSeconds { get; set; }

    public required QueueProcessingOrder QueueProcessingOrder { get; set; }

    public required int QueueLimit { get; set; }

    public required bool AutoReplenishment { get; set; }
}