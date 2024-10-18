
namespace TravelAccommodationBookingPlatform.Api.ServiceCollectionExtention;

public class CORSServiceInstaller : IServiceInstaller
{
    public IServiceCollection Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>

                options.AddDefaultPolicy(builder =>
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                        //.AllowCredentials()
                        )
            );
        return services;
    }
}
