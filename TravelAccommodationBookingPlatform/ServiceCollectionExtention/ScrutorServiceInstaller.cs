using TravelAccommodationBookingPlatform.Application.Interfaces;

namespace TravelAccommodationBookingPlatform.Api.ServiceCollectionExtention;

public class ScrutorServiceInstaller : IServiceInstaller
{
    public IServiceCollection Install(IServiceCollection services, IConfiguration configuration)
    {
        services.Scan(scan => scan
            .FromAssemblies(Infrastructure.AssemblyReference.Assembly, Persistence.AssemblyReference.Assembly)
            .AddClasses(classes => classes.AssignableTo<ITransientService>())
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        services.Scan(scan => scan
            .FromAssemblies(Infrastructure.AssemblyReference.Assembly, Persistence.AssemblyReference.Assembly)
            .AddClasses(classes => classes.AssignableTo<IScopedService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblies(Infrastructure.AssemblyReference.Assembly, Persistence.AssemblyReference.Assembly)
            .AddClasses(classes => classes.AssignableTo<ISingletonService>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        return services;
    }
}
