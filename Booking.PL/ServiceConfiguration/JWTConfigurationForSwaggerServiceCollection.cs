


using Microsoft.OpenApi.Models;

namespace Booking.PL.ServiceConfiguration;
public static class JWTConfigurationForSwaggerServiceCollection // Extension methods must be created in a non-generic static class
{
    public static IServiceCollection AddJWTConfigurationForSwaggerService(this IServiceCollection services)
    {
        // this configuration is for swagger to use JWT token for authorization
        // watch this vedio https://www.youtube.com/watch?v=aHR_E-nwGPs&list=PL3ewn8T-zRWgO-GAdXjVRh-6thRog6ddg&index=78
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Enter Bearer [space] and then your token in the text input below. Example: \"Bearer 12345abcdef\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "Bearer",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });

        return services;
    }
}
