


using Microsoft.Extensions.DependencyInjection;

namespace Booking.Shared.OptionsValidation;
public static class FluentValidationServiceExtensions
{
    public static IServiceCollection AddOptionsWithFIuentVatidation<TOptions>(this IServiceCollection services, string configurationSection)
      where TOptions : class
    {

        services.AddOptions<TOptions>() 
        // other ways
        // services.AddOptions<EmailConfig>().Bind(configuration.GetSection("EmailConfig")); 
        // services.Configure<Email>(config.GetSection("Email"));
        // Note : .Bind() method is return OptionsBuilder<TOptions> object, se it used Builder Design Pattern
        .BindConfiguration(configurationSection)
        /*
        * note : using DataAnnotations is very limited and not recommended to use
        .ValidateDataAnnotations()// check for any Data Annotations on the configuration class and perform the required checks
        */
        .ValidateFluentValidation()
        .ValidateOnStart();// this using to validate the configuration at the start of the application so we dont get an unexpected surprises at runtime


        return services;
    }
}
