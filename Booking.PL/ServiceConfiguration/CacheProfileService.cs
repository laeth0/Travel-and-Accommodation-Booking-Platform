using Microsoft.AspNetCore.Mvc;



namespace Booking.PL.ServiceConfiguration;

public static class CacheProfileService // Extension methods must be created in a non-generic static class
{
    public static IServiceCollection AddCacheProfileService(this IServiceCollection services)
    {

        services.AddControllers(options =>
        {
            options.CacheProfiles.Add("Default60Sec", new CacheProfile
            {
                Duration = 60,
                Location = ResponseCacheLocation.Client,
            });
        });

        return services;
    }
}
