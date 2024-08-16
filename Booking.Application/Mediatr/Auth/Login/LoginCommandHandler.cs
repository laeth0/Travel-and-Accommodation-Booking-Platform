using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Services;
using Booking.PL.CustomExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Booking.Application.Mediatr;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LoginCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager
        )
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;
        _signInManager = signInManager;
    }


    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken = default)
    {

        var user = await _userManager.FindByEmailAsync(request.Email)
            ?? throw new NotFoundException($"the User With Email {request.Email} is not found");

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);


        if (!result.Succeeded)
            throw new BadRequestException("Invalid Email or Password");


        var (token, validTo) = await _jwtTokenGenerator.GenerateToken(user);


        var response = new LoginResponse
        {
            UserName = user.UserName,
            Token = token,
            ValidTo = validTo
        };

        return response;
    }
}