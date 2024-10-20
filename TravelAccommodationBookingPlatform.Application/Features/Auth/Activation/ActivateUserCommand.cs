using TravelAccommodationBookingPlatform.Application.Interfaces;

namespace TravelAccommodationBookingPlatform.Application.Features.Auth.Activation;
public record ActivateUserCommand(string Token) : ICommand;
