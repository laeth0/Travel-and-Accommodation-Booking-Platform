namespace Booking.PL.CustomExceptions;
public class InternalServerErrorException(string message) : Exception(message)
{
    //Represents unexpected failures in the application.
}