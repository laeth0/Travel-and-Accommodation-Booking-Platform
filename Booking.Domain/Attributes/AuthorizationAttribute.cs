

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

public class AuthorizationAttribute : Attribute, IAuthorizationFilter
{
    private readonly string[] _userRoles;

    public AuthorizationAttribute(params string[] UserRoles)
    {
        _userRoles = UserRoles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var q = _userRoles;
        var claimsIdentity = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
        var userId = claimsIdentity?.Value;


        var userIdFromToken = context.HttpContext.User.FindAll(x => x.Type == ClaimTypes.Role);
        if (userIdFromToken == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        context.Result = new ForbidResult();
    }
}
