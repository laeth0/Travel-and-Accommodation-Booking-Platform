




namespace Booking.PL.ServiceConfiguration;
public static class ConfigService // Extension methods must be created in a non-generic static class
{

    // to learn more about this topic, visit => https://www.linkedin.com/posts/anton-martyniuk-93980994_csharp-dotnet-programming-activity-7220699033999208448-W80r?utm_source=share&utm_medium=member_desktop
    public static IServiceCollection AddConfigService(this IServiceCollection services, IConfiguration config)
    {
        var JwtOptionsBuilder = services.AddOptions<JWT>().Bind(config.GetSection("JWT")); // this way is better than => services.Configure<JWT>(config.GetSection("JWT"));
        services.AddOptions<Email>().Bind(config.GetSection("Email")); // this way is better than => services.Configure<Email>(config.GetSection("Email"));

        // Note : .Bind() method is return OptionsBuilder<TOptions> object, se it used Builder Design Pattern
        return services;
    }

}
