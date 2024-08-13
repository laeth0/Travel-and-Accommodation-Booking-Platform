




namespace Booking.PL.CustomExceptions;
public class ValidationException(string message) : Exception(message)
{
    // Use this when data validation fails.
}
