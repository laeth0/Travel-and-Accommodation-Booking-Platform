


using AutoMapper;
using Booking.Domain.Entities;
using Booking.PL.CustomExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Booking.Application.Mediatr;

public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    public GetUserProfileQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }


    public async Task<UserResponse> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {

        var user = await _userManager.FindByIdAsync(request.UserId)
                                ?? throw new NotFoundException("the user dont found");

        return _mapper.Map<UserResponse>(user);
    }
}