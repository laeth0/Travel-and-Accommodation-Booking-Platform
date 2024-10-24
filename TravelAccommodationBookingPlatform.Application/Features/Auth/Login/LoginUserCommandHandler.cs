using Microsoft.AspNetCore.Identity;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;

namespace TravelAccommodationBookingPlatform.Application.Features.Auth.Login;
public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, LoginUserResponse>
{

    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserCommandHandler(UserManager<AppUser> userManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }



    public async Task<Result<LoginUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return DomainErrors.User.EmailOrPasswordIncorrect;

        var result = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!result)
            return DomainErrors.User.EmailOrPasswordIncorrect;

        if (!user.IsActive)
            return DomainErrors.User.UserNotActivated;



        var token = await _jwtTokenGenerator.GenerateToken(user);

        var refreshToken = user.AddToken(token.Id);

        return new LoginUserResponse(token.Value, refreshToken);
    }
}
