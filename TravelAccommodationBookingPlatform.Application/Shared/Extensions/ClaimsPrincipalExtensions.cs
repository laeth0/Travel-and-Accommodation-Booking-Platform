using System.Security.Claims;

namespace TravelAccommodationBookingPlatform.Application.Shared.Extensions;
public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal? claimsPrincipal)
    {
        return claimsPrincipal?.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new ApplicationException("User Id is unavailable.");

    }
}
