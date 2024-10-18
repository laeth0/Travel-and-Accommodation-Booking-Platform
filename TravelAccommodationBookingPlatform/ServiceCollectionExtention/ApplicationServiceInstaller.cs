
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.PipelineBehavior;

namespace TravelAccommodationBookingPlatform.Api.ServiceCollectionExtention;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public IServiceCollection Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly);
            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(TransactionBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes: true);
        services.AddValidatorsFromAssembly(Infrastructure.AssemblyReference.Assembly, ServiceLifetime.Transient);

        services.AddFluentValidationAutoValidation();

        services.AddAutoMapper(Application.AssemblyReference.Assembly);

        services.Scan(scan => scan
            .FromAssemblies(Application.AssemblyReference.Assembly, Infrastructure.AssemblyReference.Assembly, Persistence.AssemblyReference.Assembly)
            .AddClasses(classes => classes.AssignableTo<ITransientService>())
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        services.Scan(scan => scan
            .FromAssemblies(Application.AssemblyReference.Assembly, Infrastructure.AssemblyReference.Assembly, Persistence.AssemblyReference.Assembly)
            .AddClasses(classes => classes.AssignableTo<IScopedService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblies(Application.AssemblyReference.Assembly, Infrastructure.AssemblyReference.Assembly, Persistence.AssemblyReference.Assembly)
            .AddClasses(classes => classes.AssignableTo<ISingletonService>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        return services;

    }
}
