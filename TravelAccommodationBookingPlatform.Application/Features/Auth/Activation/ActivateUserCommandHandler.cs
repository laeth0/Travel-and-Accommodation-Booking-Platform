using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;

namespace TravelAccommodationBookingPlatform.Application.Features.Auth.Activation;
public class ActivateUserCommandHandler : ICommandHandler<ActivateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ActivateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.AppUserRepository.GetAsync(
            user => user.ActivationCode.Value == request.Token,
            cancellationToken);

        if (user.HasNoValue)
        {
            return DomainErrors.User.UserNotFound;
        }

        if (user.Value.ActivationCode.ExpiresAtUtc < DateTime.UtcNow)
        {
            return DomainErrors.User.ActivationCodeExpired;
        }

        var activationResult = user.Value.Activate();

        return activationResult;
    }
}
