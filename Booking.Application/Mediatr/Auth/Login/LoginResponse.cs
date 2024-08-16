namespace Booking.Application.Mediatr;
public class LoginResponse
{
    public string Token { get; init; }
    public string UserName { get; init; }
    public DateTime ValidTo { get; init; }

}