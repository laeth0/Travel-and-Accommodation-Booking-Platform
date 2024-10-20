
using FluentValidation;
using TravelAccommodationBookingPlatform.Application.PipelineBehavior;

namespace TravelAccommodationBookingPlatform.Api.ServiceCollectionExtention;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public IServiceCollection Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Application.AssemblyReference.Assembly);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly);
            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(TransactionBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(Infrastructure.AssemblyReference.Assembly);
        services.AddValidatorsFromAssembly(Api.AssemblyReference.Assembly);

        //services.AddFluentValidationAutoValidation(); 
        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes: true);

        return services;
    }
}
