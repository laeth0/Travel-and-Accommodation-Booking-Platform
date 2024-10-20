using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;

namespace TravelAccommodationBookingPlatform.Application.Features.Auth.Logout;
public class LogoutUserCommandHandler : ICommandHandler<LogoutUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public LogoutUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {

        var token = await _unitOfWork.TokenRepository.GetAsync(t => t.Value == request.Token, cancellationToken);

        if (token.HasNoValue)
        {
            return DomainErrors.User.InvalidToken;
        }

        _unitOfWork.TokenRepository.Delete(token);

        return Result.Success();
    }
}
