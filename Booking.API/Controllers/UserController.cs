


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

    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult<ApiResponse>> GetAllUsers([FromHeader] string Authorization)
    {
        try
        {
            if (string.IsNullOrEmpty(Authorization))
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResponse
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Errors = new List<string> { "You should send a valid token" }
                });

            var userRoles = _serviceManager.TokenService.GetValueFromToken(Authorization).Split(',');
            if (!userRoles.Contains(nameof(UserRoles.Manager)))
                return Unauthorized(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Errors = new List<string> { "You are not authorized " }
                });

            IReadOnlyList<ApplicationUser> users = await _userManager.Users.ToListAsync();


            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "All Users",
                Result = _mapper.Map<IReadOnlyList<UserResponseDTO>>(users)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Errors = new List<string> { "Internal Server Error", ex.Message }
            });
        }
    }









   
    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult> SendEmailForForgotPassword([FromBody] string Email)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Email);
                if (user is { })
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var ResetPasswordLink = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);// Request.Scheme -> determine the port and protocol of the request dinamically

                    var message = new StringBuilder();
                    message.AppendLine("Please reset your password by clicking here: ");
                    message.AppendLine(ResetPasswordLink);

                    await _serviceManager.EmailService.SendEmailAsync(Email, "Reset Password", message.ToString());

                    var response = new SuccessResponse
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Reset Password link has been sent to your email",
                        Result = new { user.Email }
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Errors = new List<string> { "Invalid Email", "Email not Found", }
                    };
                    return NotFound(response);
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                var response = new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = errors
                };
                return BadRequest(response);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            var response = new ErrorResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Errors = new List<string> { "Internal Server Error", ex.Message }
            };
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }




    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordRequestDTO model, [FromHeader] string Authorization)
    {

        try
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is { })
                {
                    // i should check if old password dont equal new password but i will skip this step
                    var result = await _userManager.ResetPasswordAsync(user, Authorization, model.NewPassword);
                    if (result.Succeeded)
                    {
                        var response = new SuccessResponse
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Password Reset Successfully",
                            Result = new { user.Email, model.NewPassword }
                        };
                        return Ok(response);
                    }
                    else
                    {
                        var response = new ErrorResponse
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Errors = result.Errors.Select(x => x.Description).ToList()
                        };
                        return BadRequest(response);
                    }
                }
                else
                {
                    var response = new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Errors = new List<string> { "Invalid Email", "Email not Found", }
                    };
                    return NotFound(response);
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                var response = new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = errors
                };
                return BadRequest(response);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            var response = new ErrorResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Errors = new List<string> { "Internal Server Error", ex.Message }
            };
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
    */





}
