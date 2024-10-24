
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TravelAccommodationBookingPlatform.Application.Shared.Extensions;
using TravelAccommodationBookingPlatform.Application.Shared.OptionsValidation;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;
using TravelAccommodationBookingPlatform.Infrastructure.Cloudinary;
using TravelAccommodationBookingPlatform.Infrastructure.Email;
using TravelAccommodationBookingPlatform.Infrastructure.JwtToken;
using TravelAccommodationBookingPlatform.Presentation.Constants;

namespace TravelAccommodationBookingPlatform.Api.ServiceCollectionExtention;

public class InfrastructureServiceInstaller : IServiceInstaller
{
    public IServiceCollection Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptionsWithFIuentVatidation<CloudinaryConfig>(nameof(CloudinaryConfig));
        services.AddOptionsWithFIuentVatidation<EmailConfig>(nameof(EmailConfig));
        services.AddOptionsWithFIuentVatidation<JwtAuthConfig>(nameof(JwtAuthConfig));



        using var scope = services.BuildServiceProvider().CreateScope();
        var jwtAuthConfig = scope.ServiceProvider.GetRequiredService<IOptions<JwtAuthConfig>>().Value;

        var key = System.Text.Encoding.ASCII.GetBytes(jwtAuthConfig.Key);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtAuthConfig.Issuer,
            ValidAudience = jwtAuthConfig.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };

        services.AddSingleton(tokenValidationParameters);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwtBearerOptions =>
        {
            jwtBearerOptions.RequireHttpsMetadata = false;
            jwtBearerOptions.SaveToken = true;
            jwtBearerOptions.TokenValidationParameters = tokenValidationParameters;

            jwtBearerOptions.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = GetEventWhenAuthenticationFailed
            };

        });


        return services;
    }

    private static Task GetEventWhenAuthenticationFailed(AuthenticationFailedContext authenticationFailedContext)
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



}
