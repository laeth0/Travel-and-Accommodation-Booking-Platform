


using Booking.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Booking.Shared.OptionsValidation;


namespace Booking.Infrastrature.Services;
public static class EmailConfiguration // Extension methods must be created in a non-generic static class
{
    // to learn more about this topic, visit => https://www.linkedin.com/posts/anton-martyniuk-93980994_csharp-dotnet-programming-activity-7220699033999208448-W80r?utm_source=share&utm_medium=member_desktop
    public static IServiceCollection AddEmailService(this IServiceCollection services)
    {

        services.AddOptionsWithFIuentVatidation<EmailConfig>(nameof(EmailConfig));

        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
