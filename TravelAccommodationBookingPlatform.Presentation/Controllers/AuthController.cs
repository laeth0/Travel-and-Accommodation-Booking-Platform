using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Features.Auth.Activation;
using TravelAccommodationBookingPlatform.Application.Features.Auth.Login;
using TravelAccommodationBookingPlatform.Application.Features.Auth.Logout;
using TravelAccommodationBookingPlatform.Application.Features.Auth.RefreshTokens;
using TravelAccommodationBookingPlatform.Application.Features.Auth.Register;
using TravelAccommodationBookingPlatform.Application.Shared.Extensions;
using TravelAccommodationBookingPlatform.Presentation.Attributes;
using TravelAccommodationBookingPlatform.Presentation.Shared;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers;


/// <summary>
/// Controller for handling authentication related operations.
/// </summary>
public class AuthController : BaseController
{

    public AuthController(IMapper mapper, IMediator mediator) : base(mapper, mediator)
    {
    }




    /// <summary>
    /// Authenticates a user and generates a token.
    /// </summary>
    /// <param name="request">Login request with user credentials.</param>
    /// <param name="cancellationToken">Cancellation token for the request</param>
    /// <returns>A token if login is successful; otherwise, an error.</returns>
    /// <response code="200">Returns the newly created token.</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    /// <response code="401">Unauthorized if credentials are incorrect.</response>
    [HttpPost("[action]")]
    [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
    [ProducesError(StatusCodes.Status422UnprocessableEntity)]
    [ProducesError(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
                ? Ok(result.Value, "User Login Successfully")
                : result.ToProblemDetails();
    }



    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="request">Register user request with user details.</param>
    /// <param name="cancellationToken">Cancellation token for the request</param>
    /// <returns>Response indicating the result of the registration process.</returns>
    /// <response code="201">User created successfully.</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    /// <response code="409">Conflict if credentials causes a conflict.</response>
    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesError(StatusCodes.Status422UnprocessableEntity)]
    [ProducesError(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess
            ? Created()
            : result.ToProblemDetails();
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> RefreshTokens(string token, string RefreshToken)
    {
        var result = await _mediator.Send(new RefreshTokensCommand(token, RefreshToken));
        return result.IsSuccess
            ? Ok(result.Value, "Token Refreshed Successfully")
            : result.ToProblemDetails();
    }


    /// <summary>
    /// Activates a user account.
    /// </summary>
    /// <param name="token">The activation token.</param>
    /// <response code="200">The user account was activated successfully.</response>
    /// <response code="400">The activation token is invalid.</response>
    /// <returns>The activation result.</returns>
    [HttpGet("[action]")]
    public async Task<IActionResult> Activate([FromQuery] string token)
    {
        var result = await _mediator.Send(new ActivateUserCommand(token));
        return result.IsSuccess
            ? Ok("Account Activated Successfully. You can now login.", "User Account Activated Successfully")
            : result.ToProblemDetails();
    }





    /// <summary>
    /// Logs out a user.
    /// </summary>
    /// <response code="200">The user was logged out successfully.</response>
    /// <response code="400">The logout token is invalid.</response>
    /// <returns>The logout result.</returns>
    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var token = HttpContext.Request.Headers.Authorization.ToString();
        // how to check if the token is expired or not



        var result = await _mediator.Send(new LogoutUserCommand(token));
        return result.IsSuccess
            ? Ok("Logged out successfully.", "User Logged Out Successfully")
            : result.ToProblemDetails();

    }



}
