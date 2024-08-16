

using AutoMapper;
using Booking.API.CustomizeResponses;
using Booking.API.DTOs;
using Booking.Application.Mediatr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Booking.API.Controllers;

public class RoomBookingController : BaseController
{
    public RoomBookingController(IMapper mapper, ILogger<BaseController> logger, IMediator mediator)
        : base(mapper, logger, mediator)
    {
    }

    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult> GetAllBookings(CancellationToken cancellationToken = default)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var bookings = await _mediator.Send(new GetAllBookingsQuery(userId), cancellationToken);

        var response = new SuccessResponse { data = bookings };

        return Ok(response);
    }


    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult> MakeRatingForBooking([FromBody] ReviewCreateRequest model, CancellationToken cancellationToken = default)
    {
        var command = new MakeRatingForBookingCommand(model.RoomBookingId, model.Rating);

        var booking = await _mediator.Send(command, cancellationToken);

        var response = new SuccessResponse { data = booking };

        return Ok(response);
    }



    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult> MakeBooking(RoomBookingCreateRequest model, CancellationToken cancellationToken = default)
    {

        var command = _mapper.Map<MakeBookingCommand>(model);

        command.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        var booking = await _mediator.Send(command, cancellationToken);

        var response = new SuccessResponse { data = booking };

        return Ok(response);
    }




}
