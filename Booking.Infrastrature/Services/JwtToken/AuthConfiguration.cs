



using Booking.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Booking.Shared.OptionsValidation;


namespace Booking.Infrastrature.Services;
public static class AuthConfiguration // Extension methods must be created in a non-generic static class
{
    // to learn more about this topic, visit => https://www.linkedin.com/posts/anton-martyniuk-93980994_csharp-dotnet-programming-activity-7220699033999208448-W80r?utm_source=share&utm_medium=member_desktop

    public static IServiceCollection AddJwtAuthServices(this IServiceCollection services)
    {

        services.AddOptionsWithFIuentVatidation<JwtAuthConfig>(nameof(JwtAuthConfig));

        services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();



        // AddJwtAuthenticationSchemeService :- 

        // watch this vedio https://www.youtube.com/watch?v=yEQoDNHWzlE&list=PL3ewn8T-zRWgO-GAdXjVRh-6thRog6ddg&index=74


        //   على هاي التوكن Validate وكيف يعمل jwt هون انا بقول للسستم تاعي انو يدعم ال 
        // اما عملية انشاء التوكن والتحقق منها بتكون في الكلاس اللي بعمل فيه السيرفس

        //var jwtSettings = config.GetSection("JWT").Get<JWT>();


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