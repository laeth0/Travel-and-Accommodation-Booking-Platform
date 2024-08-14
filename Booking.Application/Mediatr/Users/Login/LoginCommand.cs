using MediatR;

namespace Booking.Application.Mediatr.Users.Login;

public class LoginCommand : IRequest<LoginResponse>
{
    public string Email { get; init; }
    public string Password { get; init; }
}