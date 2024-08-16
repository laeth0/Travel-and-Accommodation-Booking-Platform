

using AutoMapper;
using Booking.Domain;
using Booking.Domain.Entities;
using Booking.PL.CustomExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Booking.Application.Mediatr;
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;


    public RegisterCommandHandler(IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {

        var userByEmail = await _userManager.FindByEmailAsync(request.Email);

        if (userByEmail is { })
            throw new ValidationException("the email was already exist");


        var user = _mapper.Map<ApplicationUser>(request);

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var Errors = result.Errors
                .Select(x => x.Description)
                .Aggregate((i, j) => i + ", " + j);

            throw new BadRequestException(Errors);
        }

        var AddIdentityResult = await _userManager.AddToRoleAsync(user, nameof(ApplicationRoles.User));


        if (AddIdentityResult.Succeeded)
            return Unit.Value;


        var errors = AddIdentityResult.Errors
            .Select(x => x.Description)
            .Aggregate((i, j) => i + ", " + j);

        throw new BadRequestException(errors);

    }


}