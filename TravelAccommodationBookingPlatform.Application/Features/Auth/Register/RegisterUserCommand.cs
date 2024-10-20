
using TravelAccommodationBookingPlatform.Application.Interfaces;

namespace TravelAccommodationBookingPlatform.Application.Features.Auth.Register;

public record RegisterUserCommand(
    string Username,
    string Password,
    string Email,
    string PhoneNumber) : ICommand;
