



namespace Booking.PL.ServiceConfiguration;

public static class ConfigCORSService // Extension methods must be created in a non-generic static class
{
    public static IServiceCollection AddConfigCORSService(this IServiceCollection services)
    {
        services.AddCors(options =>

            options.AddDefaultPolicy(builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        //.AllowCredentials()
                    )
        );

        return services;
    }
}
