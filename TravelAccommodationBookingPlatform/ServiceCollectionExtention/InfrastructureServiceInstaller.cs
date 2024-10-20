
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TravelAccommodationBookingPlatform.Application.Shared.OptionsValidation;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;
using TravelAccommodationBookingPlatform.Infrastructure.Cloudinary;
using TravelAccommodationBookingPlatform.Infrastructure.Email;
using TravelAccommodationBookingPlatform.Infrastructure.JwtToken;
using TravelAccommodationBookingPlatform.Presentation.Constants;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;

namespace TravelAccommodationBookingPlatform.Api.ServiceCollectionExtention;

public class InfrastructureServiceInstaller : IServiceInstaller
{
    public IServiceCollection Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptionsWithFIuentVatidation<CloudinaryConfig>(nameof(CloudinaryConfig));
        services.AddOptionsWithFIuentVatidation<EmailConfig>(nameof(EmailConfig));
        services.AddOptionsWithFIuentVatidation<JwtAuthConfig>(nameof(JwtAuthConfig));



        // Add Jwt Authentication Scheme Service :- 

        //   على هاي التوكن Validate وكيف يعمل jwt هون انا بقول للسستم تاعي انو يدعم ال 
        // اما عملية انشاء التوكن والتحقق منها بتكون في الكلاس اللي بعمل فيه السيرفس
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwtBearerOptions =>
        {
            using var scope = services.BuildServiceProvider().CreateScope();

            var jwtAuthConfig = scope.ServiceProvider.GetRequiredService<IOptions<JwtAuthConfig>>().Value;

            var key = System.Text.Encoding.ASCII.GetBytes(jwtAuthConfig.Key);

            jwtBearerOptions.RequireHttpsMetadata = false;
            jwtBearerOptions.SaveToken = true;
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtAuthConfig.Issuer,
                ValidAudience = jwtAuthConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),// SymmetricSecurityKey is accept only byte array, so we need to convert the key to byte array
            };

            jwtBearerOptions.Events = new JwtBearerEvents
            {
                // Custom authorization failure to return ProblemDetails response
                OnAuthenticationFailed = authenticationFailedContext =>
                {
                    authenticationFailedContext.NoResult();

                    authenticationFailedContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    authenticationFailedContext.Response.ContentType = PresentationRules.ContentTypes.ProblemJson;

                    var error = PresentationErrors.AuthenticationFailed(authenticationFailedContext.Exception.Message);

                    var problemDetails = Result
                        .Failure(error)
                        .ToProblemDetails()
                        .Value as ProblemDetails;

                    return authenticationFailedContext.Response.WriteAsJsonAsync(problemDetails);
                }
            };

        });


        return services;
    }
}
