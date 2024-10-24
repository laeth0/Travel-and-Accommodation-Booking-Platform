using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Persistence.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;

namespace TravelAccommodationBookingPlatform.Application.Features.Auth.Logout;
public class LogoutUserCommandHandler : ICommandHandler<LogoutUserCommand>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public LogoutUserCommandHandler(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<Result> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {

        var token = await _refreshTokenRepository.GetAsync(t => t.Value == request.Token, cancellationToken);

        if (token.HasNoValue)
        {
            return DomainErrors.User.InvalidToken;
        }

        _refreshTokenRepository.Delete(token);

        return Result.Success();
    }
}
