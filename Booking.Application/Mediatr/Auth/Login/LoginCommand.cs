using MediatR;

namespace Booking.Application.Mediatr;

public class LoginCommand : IRequest<LoginResponse>
{
    public string Email { get; init; }
    public string Password { get; init; }
}