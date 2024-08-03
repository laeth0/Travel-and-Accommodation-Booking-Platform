using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Booking.PL.ServiceConfiguration;
public static class ConfigureApiForJWTService // Extension methods must be created in a non-generic static class
{
    public static IServiceCollection AddConfigureApiForJWTService(this IServiceCollection services, IConfiguration config)
    {
        // watch this vedio https://www.youtube.com/watch?v=yEQoDNHWzlE&list=PL3ewn8T-zRWgO-GAdXjVRh-6thRog6ddg&index=74

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(b =>
        {
            b.RequireHttpsMetadata = false;
            b.SaveToken = true;
            b.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidIssuer = config["JWT:Issuer"],
                ValidAudience = config["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["JWT:Key"])),
            };
        });

        return services;
    }
}
