using AutoMapper;
using Booking.BLL.Enums;
using Booking.BLL.Interfaces;
using Booking.BLL.IService;
using Booking.DAL.Entities;
using Booking.PL.CustomizeResponses;
using Booking.PL.DTO.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Booking.PL.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IEmailService emailService,
            ILogger<AccountController> logger
            )
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }






        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<ApiResponse>> Login([FromBody] LoginRequestDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user is { }) // equal ->  if (user != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                        if (result.Succeeded)
                        {
                            var (token, validTo) = await _tokenService.GenerateToken(user);
                            var response = new SuccessResponse
                            {
                                StatusCode = HttpStatusCode.OK,
                                Message = "Login Successfully",
                                Result = new LoginResponseDTO(user.UserName, token, validTo)
                            };
                            _logger.LogInformation($"User {user.UserName} logged in");
                            return Ok(response);
                        }
                        else
                        {
                            var response = new ErrorResponse
                            {
                                StatusCode = HttpStatusCode.BadRequest,
                                Errors = new List<string> { "Invalid login attempt", "The password in invalid" }
                            };
                            _logger.LogWarning($"Hi {user.UserName} , The password in invalid.");
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
                        _logger.LogWarning($"User with email {model.Email} not found");
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
                    _logger.LogWarning("Invalid Model State");
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
        public async Task<ActionResult<ApiResponse>> Register([FromBody] RegisterRequestDTO model)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                if (ModelState.IsValid)
                {
                    var user = _mapper.Map<ApplicationUser>(model);
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var AddIdentityResult = await _userManager.AddToRoleAsync(user, nameof(UserRoles.User));
                        if (AddIdentityResult.Succeeded)
                        {
                            await _unitOfWork.CommitAsync();
                            var registerResponse = _mapper.Map<RegisterResponseDTO>(user);
                            var response = new SuccessResponse
                            {
                                StatusCode = HttpStatusCode.OK,
                                Message = "User Created Successfully",
                                Result = registerResponse
                            };
                            _logger.LogInformation($"User {user.UserName} created successfully");
                            return Ok(response);
                        }
                        else
                        {
                            await _unitOfWork.RollbackAsync();
                            var errors = AddIdentityResult.Errors.Select(x => x.Description).ToList();
                            _logger.LogError("Error in AddToRoleAsync");
                            return BadRequest(new ErrorResponse { StatusCode = HttpStatusCode.BadRequest, Errors = errors });
                        }
                    }
                    else
                    {
                        var errors = result.Errors.Select(x => x.Description).ToList();
                        var response = new ErrorResponse
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Errors = errors
                        };
                        _logger.LogError("Error in CreateAsync");
                        return BadRequest(response);
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
                    _logger.LogWarning("Invalid Model State");
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





        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
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

                var userRole = _tokenService.GetValueFromToken(Authorization, "Role");
                if (userRole != nameof(UserRoles.Admin) && userRole != nameof(UserRoles.Manager))
                    return StatusCode(StatusCodes.Status401Unauthorized, new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.Unauthorized,
                        Errors = new List<string> { "You are not authorized to get all Users" }
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





        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
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

                var userRole = _tokenService.GetValueFromToken(Authorization, "Role");
                if (userRole != nameof(UserRoles.Admin) && userRole != nameof(UserRoles.Manager))
                    return StatusCode(StatusCodes.Status401Unauthorized, new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.Unauthorized,
                        Errors = new List<string> { "You are not authorized to get all Roles" }
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




        /*
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]

        public async Task<ActionResult<ApiResponse>> ChangeUserRole([FromBody] ChangeUserRoleRequestDTO model, [FromHeader] string Authorization)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(Authorization))
                        return StatusCode(StatusCodes.Status403Forbidden, new ErrorResponse
                        {
                            StatusCode = HttpStatusCode.Forbidden,
                            Errors = new List<string> { "You should send a valid token" }
                        });

                    var userRole = _tokenService.GetValueFromToken(Authorization, "Role");
                    if (userRole != nameof(UserRoles.Admin) && userRole != nameof(UserRoles.Manager))
                        return StatusCode(StatusCodes.Status401Unauthorized, new ErrorResponse
                        {
                            StatusCode = HttpStatusCode.Unauthorized,
                            Errors = new List<string> { "You are not authorized to change user role" }
                        });

                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user is { })
                    {
                        var oldRole = await _userManager.GetRolesAsync(user);
                        var result = await _userManager.RemoveFromRolesAsync(user, oldRole);
                        if (result.Succeeded)
                        {
                            var newRole = await _roleManager.FindByNameAsync(model.Role);
                            if (newRole is { })
                            {
                                var AddIdentityResult = await _userManager.AddToRoleAsync(user, model.Role);
                                if (AddIdentityResult.Succeeded)
                                {
                                    return Ok(new SuccessResponse
                                    {
                                        StatusCode = HttpStatusCode.OK,
                                        Message = "User Role Changed Successfully",
                                        Result = new { user.Email, model.Role }
                                    });
                                }
                                else
                                {
                                    var errors = AddIdentityResult.Errors.Select(x => x.Description).ToList();
                                    return BadRequest(new ErrorResponse { StatusCode = HttpStatusCode.BadRequest, Errors = errors });
                                }
                            }
                            else
                            {
                                return NotFound(new ErrorResponse
                                {
                                    StatusCode = HttpStatusCode.NotFound,
                                    Errors = new List<string> { "Role not Found" }
                                });
                            }
                        }
                        else
                        {
                            var errors = result.Errors.Select(x => x.Description).ToList();
                            return BadRequest(new ErrorResponse { StatusCode = HttpStatusCode.BadRequest, Errors = errors });
                        }
                    }
                    else
                    {
                        return NotFound(new ErrorResponse
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Errors = new List<string> { "Invalid Email", "Email not Found", }
                        });
                    }
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(x => x)
                }
            }
        }


        */


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

                        await _emailService.SendEmailAsync(Email, "Reset Password", message.ToString());

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






    }
}
