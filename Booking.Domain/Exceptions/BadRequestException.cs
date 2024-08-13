


namespace Booking.PL.CustomExceptions;
public class BadRequestException(string message) : Exception(message)
{
    // Represents a bad request.
}
