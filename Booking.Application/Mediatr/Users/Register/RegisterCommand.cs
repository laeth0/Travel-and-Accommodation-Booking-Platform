using MediatR;

namespace Booking.Application.Mediatr.Users.Register;
public class RegisterCommand : IRequest<Unit>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string UserName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}