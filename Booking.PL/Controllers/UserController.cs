


namespace Booking.PL.Controllers;

[ApiController, Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceManager _serviceManager;
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;

    public UserController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IUnitOfWork unitOfWork,
        IServiceManager serviceManager,
        IMapper mapper,
        ILogger<UserController> logger
        )
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        /*
         i am using Role based authorization System 
         */
        /*
          dependency يعني الاعتمادية يعني اي كلاس بعتمد على كلاس ثاني فهو بالنسبة الو dependency مصطلح 
          يعني الحقن زي كاني اعطيت الابجكت لهاد الكلاس عن طريق الحقن injection مصطلح 
        Show this vedio https://www.youtube.com/watch?v=6j3Nzr84dqo&list=PLsV97AQt78NQ8E7cEqovH0zLYRJgJahGh&index=3
         */
    }




    /// <summary>
    ///  Login the application ( this just for user login )
    /// </summary>
    /// <remarks>
    /// sample request:
    /// 
    ///     POST /api/Account/Login
    ///     {
    ///         Email = "laeth@gmail.com"
    ///         Password = "Laeth@12345"
    ///     }
    /// </remarks>
    /// <param name="model"> you should send email and password of the user</param>
    /// <returns>UserName, token, Date That Token valid To</returns>
    /// <response code="200">if the user logged in successfully</response>
    /// <response code="400">if the model is not valid</response>
    /// <response code="404">if the email not found</response>
    /// <response code="500">if there is an internal server error</response>
    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult<ApiResponse>> Login([FromBody] LoginRequestDTO model)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = ModelState.Values.SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage)
                                    .ToList()
                });

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "Invalid Email", "Email not Found", }
                });

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if (!result.Succeeded)
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "Invalid login attempt", "The password in invalid" }
                });

            var (token, validTo) = await _serviceManager.TokenService.GenerateToken(user);
            var response = new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Login Successfully",
                Result = new LoginResponseDTO(user.UserName, token, validTo)
            };

            _logger.LogInformation($"User {user.UserName} logged in");
            return Ok(response);

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

            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = ModelState.Values.SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage)
                                        .ToList()
                });

            var user = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = result.Errors.Select(x => x.Description).ToList()
                });

            var AddIdentityResult = await _userManager.AddToRoleAsync(user, nameof(UserRoles.User));

            if (!AddIdentityResult.Succeeded)
            {
                await _unitOfWork.RollbackAsync();

                var errors = AddIdentityResult.Errors.Select(x => x.Description).ToList();

                return BadRequest(new ErrorResponse { StatusCode = HttpStatusCode.BadRequest, Errors = errors });
            }

            await _unitOfWork.CommitAsync();

            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "User Created Successfully",
                Result = _mapper.Map<RegisterResponseDTO>(user)
            });


        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Errors = new List<string> { "Internal Server Error", ex.Message }
            });
        }

    }





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






    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult<ApiResponse>> Profile([FromHeader] string Authorization)
    {
        try
        {
            if (string.IsNullOrEmpty(Authorization))
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResponse
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Errors = new List<string> { "You should send a valid token" }
                });

            var userId = _serviceManager.TokenService.GetValueFromToken(Authorization, "Id");

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "User not found" }
                });

            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "User Found",
                Result = _mapper.Map<UserResponseDTO>(user)
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



    // update user profile
    [HttpPut("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult<ApiResponse>> UpdateProfile([FromForm] UpdateProfileRequestDTO model, [FromHeader] string Authorization)
    {
        try
        {
            if (string.IsNullOrEmpty(Authorization))
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResponse
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Errors = new List<string> { "You should send a valid token" }
                });

            var userId = _serviceManager.TokenService.GetValueFromToken(Authorization, "Id");

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "User not found" }
                });

            var oldUserImage = user.ImageName;
            user = _mapper.Map(model, user);

            if (model.Image is { })
                user.ImageName = await _serviceManager.FileService.UploadFile(model.Image);

            var result = await _userManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            if (!result.Succeeded)
            {
                if (model.Image is { })
                    _serviceManager.FileService.DeleteFile(user.ImageName);

                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = result.Errors.Select(x => x.Description).ToList()
                });
            }

            if (model.Image is { })
                _serviceManager.FileService.DeleteFile(oldUserImage);

            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "User Updated Successfully",
                Result = _mapper.Map<UserResponseDTO>(user)
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
