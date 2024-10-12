using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var ass = Assembly.GetExecutingAssembly();

        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(ass));

        services.AddValidatorsFromAssembly(ass);

        services.AddAutoMapper(ass);

        return services;
    }
}
