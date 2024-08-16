



using AutoMapper;
using Booking.API.CustomizeResponses;
using Booking.API.DTOs;
using Booking.Application.Mediatr;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Booking.API.Controllers;
public class AuthController : BaseController
{
    public AuthController(IMapper mapper, ILogger<BaseController> logger, IMediator mediator)
        : base(mapper, logger, mediator)
    {
        //i am using Role based authorization System 

        // this ( User.Identity.Name ) return the User Name

        //var claimsIdentity = User.Identity as ClaimsIdentity;
        //var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        //var userId = userIdClaim?.Value;
        //return Ok(userId);
        //-------------------------------------------------------------------------------------
        //var claimsIdentity = User.Claims.FirstOrDefault(x=>x.Type== ClaimTypes.NameIdentifier);
        //var userId = claimsIdentity?.Value;
        //return Ok(userId);


        /*
          dependency يعني الاعتمادية يعني اي كلاس بعتمد على كلاس ثاني فهو بالنسبة الو dependency مصطلح 
          يعني الحقن زي كاني اعطيت الابجكت لهاد الكلاس عن طريق الحقن injection مصطلح 
        Show this vedio https://www.youtube.com/watch?v=6j3Nzr84dqo&list=PLsV97AQt78NQ8E7cEqovH0zLYRJgJahGh&index=3
         */


        /*
        when putting
        [Authorize(Roles ="Manager")]
        [Authorize(Roles = "Admin")]
        this mean that the user should have the two roles to access this method (كانو اند)

        but when putting
        [Authorize(Roles = "Manager,Admin")]
        this mean that the user should have one of the two roles to access this method (كانو اور)
         */

    }



    /// <summary>
    ///  Login the application ( this just for user login )
    /// </summary>
    /// <remarks>
    /// 
    /// sample request:
    /// 
    ///     POST /Account/Login
    ///     {
    ///         "email" : "Manager@gmail.com",
    ///         "password":  "Manager@123"
    ///     }
    /// </remarks>
    /// <param name="model"> you should send email and password of the user</param>
    /// <param name="cancellationToken"></param>
    /// <returns>UserName, token, Date That Token valid To</returns>
    /// <response code="200">if the user logged in successfully</response>
    /// <response code="400">if the model is not valid (password not correct)</response>
    /// <response code="404">if the email not found</response>
    /// <response code="500">if there is an internal server error</response>
    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult> Login([FromBody] LoginRequest model, CancellationToken cancellationToken = default)
    {

        var command = _mapper.Map<LoginCommand>(model);

        var UserData = await _mediator.Send(command, cancellationToken);

        var response = new SuccessResponse { data = UserData, Message = "User Logged in Successfully" };

        return Ok(response);

    }





    /// <summary>
    ///   Processes registering a guest request.
    /// </summary>
    /// <remarks>
    /// 
    /// sample request:
    /// 
    ///     POST /Account/Register
    ///     {
    ///         "firstName": "Laeth",
    ///         "lastName": "Nueirat",
    ///         "userName": "Laeth_Raed",
    ///         "email": "Laeth@gmail.com",
    ///         "password": "Laeth@123"
    ///     }
    /// </remarks>
    /// <param name="model"> The First Name, Last Name, Email, and Password of the user</param>
    /// <param name="cancellationToken"></param>
    /// <returns>it will not return any thing</returns>
    /// <response code="200">if the user Register successfully</response>
    /// <response code="400">if the model is not valid</response>
    /// <response code="500">if there is an internal server error</response>
    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult> Register([FromBody] RegisterRequest model, CancellationToken cancellationToken = default)
    {

        var command = _mapper.Map<RegisterCommand>(model);

        await _mediator.Send(command, cancellationToken);

        var response = new SuccessResponse { Message = "User Registered Successfully" };

        return Ok(response);
    }









}
