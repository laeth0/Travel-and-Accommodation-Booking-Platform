using Microsoft.Extensions.Options;

namespace TravelAccommodationBookingPlatform.Api.Options;

public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
{
    private readonly IConfiguration _configuration;
    private const string ConfigurationSectionName = "DatabaseOptions";

    public DatabaseOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(DatabaseOptions options)
    {
        options.ConnectionString = _configuration.GetConnectionString("DefaultConnection") ??
                                  throw new ArgumentException("Connection string 'DefaultConnection' not found.");

        _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
