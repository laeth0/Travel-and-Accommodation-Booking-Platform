using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
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

            var (statusCode, message) = GetStatusCodeAndMessage(exceptionHandlerPathFeature!);

            return Results.Problem(message, statusCode: statusCode);
        });

        return app;
    }

    private static (int, string) GetStatusCodeAndMessage(Exception exception)
    {
        switch (exception)
        {
            case DomainException:
                return (StatusCodes.Status400BadRequest, exception.Message);
            case UniqueConstraintException:
                {
                    var uniqueConstraintException = (UniqueConstraintException)exception;

                    string message = "";

                    foreach (var prop in uniqueConstraintException.Entries)
                        message += $"The value '{prop.Entity}' already exists.\n";

                    return (StatusCodes.Status400BadRequest, message);
                }
            case CannotInsertNullException:
                {
                    var cannotInsertNullException = (CannotInsertNullException)exception;

                    string message = "";

                    foreach (var prop in cannotInsertNullException.Entries)
                        message += $"The value '{prop.Entity}' cannot be null.\n";

                    return (StatusCodes.Status422UnprocessableEntity, message);
                }
            case MaxLengthExceededException:
                {
                    var maxLengthExceededException = (MaxLengthExceededException)exception;
                    string message = "";

                    foreach (var prop in maxLengthExceededException.Entries)
                        message += $"The value '{prop.Entity}' exceeds the maximum length.\n";

                    return (StatusCodes.Status422UnprocessableEntity, message);
                }
            case NumericOverflowException:
                {
                    var numericOverflowException = (NumericOverflowException)exception;
                    string message = "";

                    foreach (var prop in numericOverflowException.Entries)
                        message += $"The value '{prop.Entity}' exceeds the maximum value.\n";

                    return (StatusCodes.Status422UnprocessableEntity, message);
                }
            case ReferenceConstraintException:
                {
                    var referenceConstraintException = (ReferenceConstraintException)exception;

                    string message = "";

                    foreach (var prop in referenceConstraintException.Entries)
                        message += $"The value '{prop.Entity}' does not exist.\n";

                    message += referenceConstraintException.ConstraintName.ToString();

                    return (StatusCodes.Status400BadRequest, message);
                }
            case DbUpdateException:
                return (StatusCodes.Status400BadRequest, "An error occurred while updating the database.");
            default:
                return (StatusCodes.Status500InternalServerError, exception.Message);
        }
    }
}


