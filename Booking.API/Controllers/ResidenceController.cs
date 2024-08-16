


using AutoMapper;
using Booking.API.CustomizeResponses;
using Booking.API.DTOs;
using Booking.Application.Mediatr;
using Booking.Domain.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Booking.API.Controllers;

public class ResidenceController : BaseController
{
    public ResidenceController(IMapper mapper, ILogger<BaseController> logger, IMediator mediator)
            : base(mapper, logger, mediator)
    {
    }





    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    //[ResponseCache(CacheProfileName = "Default60Sec")] //[ResponseCache(Duration =60,Location =ResponseCacheLocation.Client)]
    public async Task<ActionResult> GetAllResidenceType(CancellationToken cancellationToken = default)
    {

        var query = new GetAllResidenceTypeQuery();

        var ResidenceTypes = await _mediator.Send(query, cancellationToken);

        var response = new SuccessResponse { data = ResidenceTypes };

        return Ok(response);
    }






    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    //[ResponseCache(CacheProfileName = "Default60Sec")] //[ResponseCache(Duration =60,Location =ResponseCacheLocation.Client)]
    public async Task<ActionResult> Index(CancellationToken cancellationToken = default)
    {

        var response = new SuccessResponse
        {
            data = await _mediator.Send(new GetAllResidenceQuery(), cancellationToken)
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
            data = await _mediator.Send(new GetSpecificResidenceQuery(id), cancellationToken)
        };

        return Ok(response);

    }




    [HttpGet("[action]/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult> GetRoomForSpecificResidence(Guid id, CancellationToken cancellationToken = default)
    {

        var query = new GetRoomForSpecificResidenceQuery(id);

        var data = await _mediator.Send(query, cancellationToken);

        var response = new SuccessResponse { data = data };

        return Ok(response);

    }





    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]// if the user role is not Admin or Manager
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))] // if there is no token(no role)
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult> Create([FromForm] ResidenceCreateRequest model, CancellationToken cancellationToken = default)
    {
        // show this vedio https://www.youtube.com/watch?v=pDtDEVbEDdQ&list=PL3ewn8T-zRWgO-GAdXjVRh-6thRog6ddg&index=16

        var ResidenceCommand = _mapper.Map<CreateResidenceCommand>(model);

        var ResidenceResponse = await _mediator.Send(ResidenceCommand, cancellationToken);

        var response = new SuccessResponse
        {
            StatusCode = HttpStatusCode.Created,
            data = ResidenceResponse,
            Message = ResidenceMessages.ResidenceCreated
        };

        return CreatedAtAction(nameof(Details), new { id = ResidenceResponse.Id }, response);

    }






    [HttpPut("[action]/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult> Update(Guid id, [FromForm] ResidenceCreateRequest model, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<UpdateResidenceCommand>(model,
            opts => opts.AfterMap((src, dest) => dest.Id = id));

        var response = new SuccessResponse
        {
            data = await _mediator.Send(command, cancellationToken),
            Message = ResidenceMessages.ResidenceUpdated
        };

        return Ok(response);

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

        await _mediator.Send(new DeleteResidenceCommand(id), cancellationToken);

        var response = new SuccessResponse
        {
            Message = ResidenceMessages.ResidenceDeleted
        };

        return Ok(response);

    }


}
