using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Exceptions;

namespace TravelAccommodationBookingPlatform.Domain.Shared;
public static class Ensure
{
    public static void NotEmpty(string value, string? message, string? argumentName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(message, argumentName);
        }
    }

    public static void NotDefault<TValue>(TValue value, string? message, string? argumentName)
    {
        var error = $"{argumentName} {message}";

        if (EqualityComparer<TValue>.Default.Equals(value, default))
        {
            throw new DomainException(DomainErrors.General.NotDefault.WithMessage(error));
        }
    }

    public static void NotPast(DateTime value, string? message, string? argumentName)
    {
        var error = $"{argumentName} {message}";
        if (value < DateTime.UtcNow)
        {
            throw new DomainException(DomainErrors.General.NotPast.WithMessage(error));
        }
    }

}