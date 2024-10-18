
using System.Text.Json.Serialization;

namespace TravelAccommodationBookingPlatform.Api.ServiceCollectionExtention;

public class PresentationServiceInstaller : IServiceInstaller
{
    public IServiceCollection Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
            .AddApplicationPart(Presentation.AssemblyReference.Assembly)
            .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        return services;
    }
}
