using TravelAccommodationBookingPlatform.Application.Interfaces;

namespace TravelAccommodationBookingPlatform.Application.Features.Auth.Login;
public class LoginUserCommand : ICommand<LoginUserResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
