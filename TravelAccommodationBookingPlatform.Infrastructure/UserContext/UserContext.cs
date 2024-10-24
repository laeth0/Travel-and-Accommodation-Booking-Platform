using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TravelAccommodationBookingPlatform.Application.Interfaces.Services;
using TravelAccommodationBookingPlatform.Domain.Enums;

namespace TravelAccommodationBookingPlatform.Infrastructure.UserContext;
public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor) =>
        _httpContextAccessor = httpContextAccessor;


    public string Id =>
            _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new ApplicationException("The user identifier claim is required.");



    public UserRoles Role =>
        Enum.Parse<UserRoles>(
            _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value
                ?? throw new ApplicationException("The user role claim is required.")
        );


    public string Email =>
        _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value
                ?? throw new ApplicationException("The user email claim is required.");


    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated
                ?? throw new ApplicationException("User context is unavailable");

}
