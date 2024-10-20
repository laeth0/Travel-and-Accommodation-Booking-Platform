using TravelAccommodationBookingPlatform.Application.Interfaces;

namespace TravelAccommodationBookingPlatform.Application.Features.Auth.Logout;
public record LogoutUserCommand(string Token) : ICommand;
