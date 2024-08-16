


using AutoMapper;
using Booking.API.CustomizeResponses;
using Booking.API.DTOs;
using Booking.Application.Mediatr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Booking.API.Controllers;

public class RoomController : BaseController
{
    public RoomController(IMapper mapper, ILogger<BaseController> logger, IMediator mediator)
     : base(mapper, logger, mediator)
    {


        /*
        if (!ModelState.IsValid)
            return BadRequest(new ErrorResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Errors = ModelState.Values
                            .SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage)
                            .ToList()
            });
        */
    }




    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    //[ResponseCache(CacheProfileName = "Default60Sec")] //[ResponseCache(Duration =60,Location =ResponseCacheLocation.Client)]
    public async Task<ActionResult> GetAllRoomType(CancellationToken cancellationToken = default)
    {

        var query = new GetAllRoomTypeQuery();

        var RoomTypes = await _mediator.Send(query, cancellationToken);

        var response = new SuccessResponse { data = RoomTypes };

        return Ok(response);

    }




    /*
    /// <summary>
    ///  get all rooms with optional pagination 
    /// </summary>
    /// <remarks>
    /// sample request:
    /// 
    ///     GET /api/Room
    ///     {
    ///     }
    /// </remarks>
    /// <param name="PageSize"></param>
    /// <param name="PageNumber"></param>
    /// <returns>a list of rooms</returns>
    /// <response code="200">if the rooms are retrieved successfully</response>
    /// <response code="500">if there is an internal server error</response>
    */
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    //[ResponseCache(CacheProfileName = "Default60Sec")] //[ResponseCache(Duration =60,Location =ResponseCacheLocation.Client)]
    public async Task<ActionResult> Index(CancellationToken cancellationToken = default)
    {

        var response = new SuccessResponse
        {
            data = await _mediator.Send(new GetAllRoomQuery(), cancellationToken)
        };

        return Ok(response);

    }


    [HttpGet("[action]/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult> Details(Guid id, CancellationToken cancellationToken = default)
    {

        var response = new SuccessResponse
        {
            data = await _mediator.Send(new GetSpecificRoomQuery(id), cancellationToken)
        };

        return Ok(response);

    }





    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]// if the user role is not Admin or Manager
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))] // if there is no token(no role)
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult> Create([FromForm] RoomCreateRequest model, CancellationToken cancellationToken = default)
    {
        // show this vedio https://www.youtube.com/watch?v=pDtDEVbEDdQ&list=PL3ewn8T-zRWgO-GAdXjVRh-6thRog6ddg&index=16

        var RoomCommand = _mapper.Map<CreateRoomCommand>(model);

        var RoomResponse = await _mediator.Send(RoomCommand, cancellationToken);

        var response = new SuccessResponse
        {
            StatusCode = HttpStatusCode.Created,
            data = RoomResponse,
            Message = "Room Greated Successfully"
        };

        return CreatedAtAction(nameof(Details), new { id = RoomResponse.Id }, response);

    }





    [HttpDelete("[action]/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {

        await _mediator.Send(new DeleteRoomCommand(id), cancellationToken);

        var response = new SuccessResponse
        {
            Message = "Room Deleted Successfully"
        };

        return Ok(response);

    }




}
