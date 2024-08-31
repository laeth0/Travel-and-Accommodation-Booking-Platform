



using Booking.Domain.Interfaces.Services;
using Booking.Shared.OptionsValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Booking.Infrastrature.Services;
public static class AuthConfiguration  // Extension methods must be created in a non-generic static class
{
    public static IServiceCollection AddJwtAuthServices(this IServiceCollection services)
    {

        services.AddOptionsWithFIuentVatidation<JwtAuthConfig>(nameof(JwtAuthConfig));

        services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();


        // Add Jwt Authentication Scheme Service :- 

        //   على هاي التوكن Validate وكيف يعمل jwt هون انا بقول للسستم تاعي انو يدعم ال 
        // اما عملية انشاء التوكن والتحقق منها بتكون في الكلاس اللي بعمل فيه السيرفس
        services.AddAuthentication(options =>
        {
            // here i put JwtBearer as the default authentication scheme
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;


            // if the jwt is not default authentication scheme, we should writre => AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, b =>
        }).AddJwtBearer(jwtBearerOptions =>
        {

            using var scope = services.BuildServiceProvider().CreateScope();

            var jwtAuthConfig = scope.ServiceProvider.GetRequiredService<IOptions<JwtAuthConfig>>().Value;

            var key = Encoding.ASCII.GetBytes(jwtAuthConfig.Key);

            jwtBearerOptions.RequireHttpsMetadata = false;
            jwtBearerOptions.SaveToken = true;
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidIssuer = jwtAuthConfig.Issuer,
                ValidateAudience = false,
                ValidAudience = jwtAuthConfig.Audience,

                ValidateIssuerSigningKey = true,

                IssuerSigningKey = new SymmetricSecurityKey(key),// SymmetricSecurityKey is accept only byte array, so we need to convert the key to byte array

                ValidateLifetime = true,
            };
        });


        return services;
    }
}