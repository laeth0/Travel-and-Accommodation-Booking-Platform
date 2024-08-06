


using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Booking.PL.ServiceConfiguration;
public static class ConfigurationForSwaggerServiceCollection // Extension methods must be created in a non-generic static class
{
    public static IServiceCollection AddConfigurationForSwaggerService(this IServiceCollection services)
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




            // show this vedio  https://www.youtube.com/watch?v=lml_j5ujjeQ
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Travel and Accommodation Booking Platform",
                Description = "Travel and Accommodation Booking Platform a comprehensive project transforming online hotel reservations. Dive into clean, secure APIs, emphasizing RESTful design, robust error handling, and secure JWT authentication.",
                Contact = new OpenApiContact
                {
                    Name = "Laeth Nueirat",
                    Email = "laethraed0@gmail.com",
                    Url = new Uri("https://www.linkedin.com/in/laethnueirat"),
                }
            });

            var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
            options.IncludeXmlComments(xmlCommentsFullPath); 

        });

        return services;
    }
}
