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


}