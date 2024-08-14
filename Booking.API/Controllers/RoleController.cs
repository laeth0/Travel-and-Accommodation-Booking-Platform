


using AutoMapper;
using MediatR;

namespace Booking.API.Controllers;

public class RoleController : BaseController
{
    public RoleController(IMapper mapper, ILogger<BaseController> logger, IMediator mediator)
     : base(mapper, logger, mediator)
    {
    }





    /*

    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult<ApiResponse>> GetAllRoles([FromHeader] string Authorization)
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
                    Errors = new List<string> { "You are not authorized" }
                });

            IReadOnlyList<IdentityRole> Roles = await _roleManager.Roles.ToListAsync();

            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "All Roles",
                Result = _mapper.Map<IReadOnlyList<RolesResponseDTO>>(Roles)
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
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult<ApiResponse>> AddUserToRole([FromBody] ChangeUserRoleDTO model, [FromHeader] string Authorization)
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
                    Errors = new List<string> { "You are not authorized to change user role" }
                });

            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(model.RoleName))
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "Invalid User Id or Role Name" }
                });


            if (await _userManager.IsInRoleAsync(user, model.RoleName))
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "User already has this role" }
                });


            var result = await _userManager.AddToRoleAsync(user, model.RoleName);
            if (!result.Succeeded)
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = result.Errors.Select(x => x.Description).ToList()
                });

            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "User Role Changed Successfully",
                Result = new ChangeUserRoleDTO { UserId = user.Id, RoleName = model.RoleName }
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
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult<ApiResponse>> RemoveUserFromRole([FromBody] ChangeUserRoleDTO model, [FromHeader] string Authorization)
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
                    Errors = new List<string> { "You are not authorized to change user role" }
                });


            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(model.RoleName))
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "Invalid User Id or Role Name" }
                });


            if (!await _userManager.IsInRoleAsync(user, model.RoleName))
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "User doesn't have this role" }
                });


            var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
            if (!result.Succeeded)
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = result.Errors.Select(x => x.Description).ToList()
                });

            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "User Role Removed Successfully",
                Result = new ChangeUserRoleDTO { UserId = user.Id, RoleName = model.RoleName }
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


    */






}
