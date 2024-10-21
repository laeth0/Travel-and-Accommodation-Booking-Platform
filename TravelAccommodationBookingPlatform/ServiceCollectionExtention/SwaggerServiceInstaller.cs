using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace TravelAccommodationBookingPlatform.Api.ServiceCollectionExtention;

public class SwaggerServiceInstaller : IServiceInstaller
{
    public IServiceCollection Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Enter Bearer [space] and then your token in the text input below.",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
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

            //var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
            //options.IncludeXmlComments(xmlCommentsFullPath);

        });


        return services;
    }
}
