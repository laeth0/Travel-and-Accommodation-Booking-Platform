namespace Booking.PL.CustomExceptions;
public class NotFoundException(string message) : Exception(message)
{
    // This exception is thrown when a requested resource is not found.
}
