


using Booking.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Booking.Infrastrature.Services.Email;
public static class EmailConfiguration // Extension methods must be created in a non-generic static class
{
    // to learn more about this topic, visit => https://www.linkedin.com/posts/anton-martyniuk-93980994_csharp-dotnet-programming-activity-7220699033999208448-W80r?utm_source=share&utm_medium=member_desktop
    public static IServiceCollection AddEmailService(this IServiceCollection services)
    {
        services.AddOptions<EmailConfig>()
          .BindConfiguration(nameof(EmailConfig));

        // other ways
        // services.AddOptions<EmailConfig>().Bind(configuration.GetSection("EmailConfig")); 
        // services.Configure<Email>(config.GetSection("Email"));
        // Note : .Bind() method is return OptionsBuilder<TOptions> object, se it used Builder Design Pattern


        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
