using Microsoft.Extensions.DependencyInjection;

namespace TravelAccommodationBookingPlatform.Application.Shared.OptionsValidation;
public static class FluentValidationServiceExtensions
{
    public static IServiceCollection AddOptionsWithFIuentVatidation<TOptions>(this IServiceCollection services, string configurationSection)
      where TOptions : class
    {

        services.AddOptions<TOptions>()
        .BindConfiguration(configurationSection)
        .ValidateFluentValidation()
        .ValidateOnStart();

        return services;
    }
}