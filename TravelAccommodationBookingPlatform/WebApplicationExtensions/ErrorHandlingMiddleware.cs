using Microsoft.AspNetCore.Diagnostics;

namespace TravelAccommodationBookingPlatform.Api.WebApplicationExtensions;

public static class ErrorHandlingMiddleware
{
    public static WebApplication UseGlobalErrorHandling(this WebApplication app)
    {
        app.UseExceptionHandler("/error");

        app.Map("error", (HttpContext httpContext) =>
        {
            var exceptionHandlerPathFeature = httpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error;

            var StatusCode = StatusCodes.Status500InternalServerError;

            return Results.Problem(exceptionHandlerPathFeature?.Message, statusCode: StatusCode, instance: $"{httpContext.Request.Method} {httpContext.Request.Path}");
        });

        return app;
    }
}


