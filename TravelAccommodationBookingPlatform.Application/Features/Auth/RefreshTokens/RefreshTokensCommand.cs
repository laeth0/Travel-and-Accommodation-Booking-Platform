using TravelAccommodationBookingPlatform.Application.Interfaces;

namespace TravelAccommodationBookingPlatform.Application.Features.Auth.RefreshTokens;
public record RefreshTokensCommand(string token, string refreshToken) : ICommand<RefreshTokensResponse>;
