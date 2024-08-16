


using AutoMapper;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Services;
using Booking.PL.CustomExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Booking.Application.Mediatr;

public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, UserResponse>
{
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly UserManager<ApplicationUser> _userManager;


    public UpdateUserProfileCommandHandler(IMapper mapper, UserManager<ApplicationUser> userManager, ICloudinaryService cloudinaryService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<UserResponse> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {

        var user = await _userManager.FindByIdAsync(request.UserId)
                                    ?? throw new NotFoundException("User Not found");

        _mapper.Map(request, user);

        if (request.Image is { })
            (user.ImagePublicId, user.ImageUrl) = await _cloudinaryService.UploadImageAsync(request.Image, cancellationToken);


        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
            return _mapper.Map<UserResponse>(user);

        if (request.Image is { })
            await _cloudinaryService.DeleteImageAsync(user.ImagePublicId);


        var errors = result.Errors.Select(e => e.Description).Aggregate((a, b) => $"{a}, {b}");
        throw new Exception(errors);
    }
}
