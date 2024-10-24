
using System.Globalization;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TravelAccommodationBookingPlatform.Api.RateLimiting;
using TravelAccommodationBookingPlatform.Application.Shared.Extensions;
using TravelAccommodationBookingPlatform.Application.Shared.OptionsValidation;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;
using TravelAccommodationBookingPlatform.Presentation.Constants;

namespace TravelAccommodationBookingPlatform.Api.ServiceCollectionExtention;

public class RateLimitingServiceInstaller : IServiceInstaller
{
    public IServiceCollection Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptionsWithFIuentVatidation<FixedWindowRateLimiterConfig>(nameof(FixedWindowRateLimiterConfig));

        services.AddRateLimiter(rateLimitingOptions =>
        {
            using var scope = services.BuildServiceProvider().CreateScope();

            var rateLimiterOptions = scope.ServiceProvider.GetRequiredService<IOptions<FixedWindowRateLimiterConfig>>().Value;

            var fixedWindowRateLimiterOptions = new FixedWindowRateLimiterOptions
            {
                PermitLimit = rateLimiterOptions.PermitLimit,
                Window = TimeSpan.FromSeconds(rateLimiterOptions.TimeWindowSeconds),
                AutoReplenishment = rateLimiterOptions.AutoReplenishment,
                QueueProcessingOrder = rateLimiterOptions.QueueProcessingOrder,
                QueueLimit = rateLimiterOptions.QueueLimit,
            };

            rateLimitingOptions.AddPolicy(PresentationRules.RateLimitPolicies.Fixed, httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                     httpContext.Connection.RemoteIpAddress?.ToString(),
                     _ => fixedWindowRateLimiterOptions));

            rateLimitingOptions.OnRejected = async (onRejectedContext, cancellationToken) =>
            {
                onRejectedContext.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                onRejectedContext.HttpContext.Response.ContentType = PresentationRules.ContentTypes.ProblemJson;

                var problemDetails = Result
                   .Failure(PresentationErrors.TooManyRequests)
                   .ToProblemDetails()
                   .Value as ProblemDetails;

                if (onRejectedContext.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    onRejectedContext.HttpContext.Response.Headers.RetryAfter = retryAfter.TotalSeconds.ToString(CultureInfo.InvariantCulture);
                    problemDetails!.Extensions["retryAfter"] = retryAfter.TotalSeconds;
                }

                await onRejectedContext.HttpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            };

        });

        return services;
    }
}
