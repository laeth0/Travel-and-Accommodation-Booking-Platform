
namespace TravelAccommodationBookingPlatform.Api.Options;

public class DatabaseOptions
{
    public string ConnectionString { get; set; }

    public bool EnableDetailedErrors { get; set; }

    public bool EnableSensitiveDataLogging { get; set; }

    public int CommandTimeout { get; set; }

    public int MaxRetryCount { get; set; }


}
