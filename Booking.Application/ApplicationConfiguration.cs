


using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Booking.Application;
public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(assembly));


        /*
        Automatic Discovery approach to register AutoMapper profiles:-
            •	Automatic Discovery: This approach tells AutoMapper to scan the specified assembly (in this case, the assembly where the code is executing) for all classes that inherit from Profile.
            •	Less Maintenance: You don't need to manually list each profile class. If you add new profiles in the future, they will be automatically discovered and registered without modifying this line of code.
            •	Potential Overhead: It might scan more classes than necessary if the assembly contains many classes, but this is usually negligible.
        */
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        /*
        Explicit Registration:
            •	Explicit Registration: This approach explicitly registers each profile class. You list each profile class you want to include.
            •	More Control: You have precise control over which profiles are registered. This can be useful if you only want to include specific profiles and exclude others.
            •	Manual Maintenance: You need to update this list whenever you add or remove profiles, which can be more error-prone and require more maintenance.
         */
        //services.AddAutoMapper(typeof(CityProfile), typeof(CountryProfile));


        services.AddValidatorsFromAssembly(assembly);


        return services;
    }
}
