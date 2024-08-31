


using AutoMapper;
using Booking.API.CustomizeResponses;
using Booking.API.DTOs;
using Booking.Application.Mediatr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Booking.API.Controllers;

public class UserController : BaseController
{
    public UserController(IMapper mapper, ILogger<BaseController> logger, IMediator mediator)
        : base(mapper, logger, mediator)
    {
    }


    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult> Profile()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var Query = new GetUserProfileQuery(userId);
        var result = await _mediator.Send(Query);
        var response = new SuccessResponse { Message = "User Profile", data = result };
        return Ok(response);
    }


    [HttpPut("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult> UpdateUserProfile([FromForm] UpdateProfileRequest model)
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        var command = _mapper.Map<UpdateUserProfileCommand>(model,
  opts => opts.AfterMap((src, dest) => dest.UserId = userId));

        var result = await _mediator.Send(command);

        var response = new SuccessResponse { Message = "User Profile Updated", data = result };

        return Ok(response);
    }





    /*
    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult> SendEmailForForgotPassword([FromBody] string Email)
    {
        var user = await _userManager.FindByEmailAsync(Email);

        if (user is { })
            return NotFound();

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var ResetPasswordLink = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);// Request.Scheme -> determine the port and protocol of the request dinamically

        string message = "Please reset your password by clicking here: " + ResetPasswordLink;

        await _serviceManager.EmailService.SendEmailAsync(Email, "Reset Password", message);

        return Ok();
    }


    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordRequestDTO model, [FromHeader] string Authorization)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null)
            return NotFound();

        // i should check if old password dont equal new password but i will skip this step
        var result = await _userManager.ResetPasswordAsync(user, Authorization, model.NewPassword);

        if (result.Succeeded)
            return Ok();
    }
    */


}





}
