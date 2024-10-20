using Microsoft.AspNetCore.Diagnostics;
using TravelAccommodationBookingPlatform.Domain.Exceptions;

namespace TravelAccommodationBookingPlatform.Api.WebApplicationExtensions;

public static class ErrorHandlingMiddleware
{
    public static WebApplication UseGlobalErrorHandling(this WebApplication app)
    {
        app.UseExceptionHandler("/error");

        app.Map("error", (HttpContext httpContext) =>
        {
            var exceptionHandlerPathFeature = httpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error;

            var statusCode = exceptionHandlerPathFeature switch
            {
                DomainException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            return Results.Problem(
                exceptionHandlerPathFeature?.Message,
                "{httpContext.Request.Method} {httpContext.Request.Path}",
                statusCode);
        });

        return app;
    }
}


