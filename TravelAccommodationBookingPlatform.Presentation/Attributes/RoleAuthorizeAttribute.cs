using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using TravelAccommodationBookingPlatform.Application.Shared.Extensions;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;
using UserRoles = TravelAccommodationBookingPlatform.Domain.Enums.UserRoles;

namespace TravelAccommodationBookingPlatform.Presentation.Attributes;
public class RoleAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly UserRoles[] _roles;

    public RoleAuthorizeAttribute() : this(Enum.GetValues<UserRoles>())
    {
    }

    public RoleAuthorizeAttribute(params UserRoles[] roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (user.Identity is { IsAuthenticated: false })
        {
            context.Result = Result
                .Failure(DomainErrors.User.CredentialsNotProvided)
                .ToProblemDetails();
            return;
        }

        var userRoles = user.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => Enum.Parse<UserRoles>(c.Value))
            .ToList();


        if (!_roles.Intersect(userRoles).Any())
        {
            context.Result = Result
                .Failure(DomainErrors.User.InvalidRole)
                .ToProblemDetails();
        }

    }
}