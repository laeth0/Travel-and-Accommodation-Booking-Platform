

namespace Booking.PL.CustomExceptions;

public class UnauthorizedException(string message) : Exception(message)
{
    // Indicates unauthorized access to a resource.
}