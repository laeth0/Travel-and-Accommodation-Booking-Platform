


using Booking.Application.Mediatr;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Booking.Application;
public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        /*
         •	Purpose: Retrieves the assembly that contains the code that is currently executing.
         •	Context: This is dynamic and depends on where the code is being executed. If called from within a library, it will return the library's assembly. If called from an application, it will return the application's assembly.
         */
        var assembly = Assembly.GetExecutingAssembly();

        /*
        •	Purpose: Retrieves the assembly that contains the ApplicationConfiguration type.
        •	Use Case: Useful when you need to get information about the assembly that defines a specific type, regardless of where the code is being executed.
        •	Context: This is static and tied to the ApplicationConfiguration type. It will always return the assembly where ApplicationConfiguration is defined, regardless of where the code is running.
         */
        var ApplicationConfigurationAssembly = typeof(ApplicationConfiguration).Assembly;

        /*
         Conclusion :-
        •	Assembly.GetExecutingAssembly(): Retrieves the assembly of the currently executing code.
        •	typeof(ApplicationConfiguration).Assembly: Retrieves the assembly where the ApplicationConfiguration type is defined.
         */


        //--------------------------------------------------------------------------------------------------------------

        services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(assembly));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        //--------------------------------------------------------------------------------------------------------------

        /*
        Automatic Discovery approach to register AutoMapper profiles:-
            •	Automatic Discovery: This approach tells AutoMapper to scan the specified assembly (in this case, the assembly where the code is executing) for all classes that inherit from Profile.
            •	Less Maintenance: You don't need to manually list each profile class. If you add new profiles in the future, they will be automatically discovered and registered without modifying this line of code.
            •	Potential Overhead: It might scan more classes than necessary if the assembly contains many classes, but this is usually negligible.
        */
        //services.AddAutoMapper(assembly);

        /*
        Explicit Registration:
            •	Explicit Registration: This approach explicitly registers each profile class. You list each profile class you want to include.
            •	More Control: You have precise control over which profiles are registered. This can be useful if you only want to include specific profiles and exclude others.
            •	Manual Maintenance: You need to update this list whenever you add or remove profiles, which can be more error-prone and require more maintenance.
         */
        //services.AddAutoMapper(typeof(CityProfile), typeof(CountryProfile));


        return services;
    }
}
