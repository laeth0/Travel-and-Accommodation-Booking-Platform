using Microsoft.AspNetCore.Identity;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Features.Auth.Register;
public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {

        var activationCode = ActivationCode.Create(
            Guid.NewGuid().ToString().Replace("-", string.Empty),
            DateTime.UtcNow.AddHours(24)
        );

        var user = new AppUser
        {
            UserName = request.Username,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            ActivationCode = activationCode
        };

        var createUserResult = await _userManager.CreateAsync(user, request.Password);

        if (!createUserResult.Succeeded)
        {
            return Result.Failure(createUserResult.Errors.First());
        }

        var addToRoleResult = await _userManager.AddToRoleAsync(user, UserRoles.User);

        if (!addToRoleResult.Succeeded)
        {
            return Result.Failure(addToRoleResult.Errors.First());
        }

        return Result.Success();

    }
}
