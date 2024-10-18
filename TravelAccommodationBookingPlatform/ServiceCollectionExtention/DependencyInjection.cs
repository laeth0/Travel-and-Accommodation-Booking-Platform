using System.Reflection;

namespace TravelAccommodationBookingPlatform.Api.ServiceCollectionExtention;

public static class DependencyInjection
{
    public static IServiceCollection InstallServices(this IServiceCollection services
        , IConfiguration configuration,
        params Assembly[] assemblies)
    {

        var serviceInstallers = assemblies.SelectMany(assembly => assembly.DefinedTypes)
              .Where(IsAssignableToType<IServiceInstaller>)
              .Select(Activator.CreateInstance)
              .Cast<IServiceInstaller>();


        foreach (var serviceInstaller in serviceInstallers)
        {
            serviceInstaller.Install(services, configuration);
        }


        return services;
    }

    private static bool IsAssignableToType<T>(TypeInfo typeInfo)
    {
        return typeof(T).IsAssignableFrom(typeInfo)
            && !typeInfo.IsInterface
            && !typeInfo.IsAbstract;
    }
}
