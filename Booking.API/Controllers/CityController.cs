





using AutoMapper;
using Booking.API.CustomizeResponses;
using Booking.API.DTOs;
using Booking.Application.Mediatr;
using Booking.Domain.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Booking.API.Controllers;

public class CityController : BaseController
{

    public CityController(IMapper mapper, ILogger<BaseController> logger, IMediator mediator)
        : base(mapper, logger, mediator)
    {
    }





    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    //[ResponseCache(CacheProfileName = "Default60Sec")] //[ResponseCache(Duration =60,Location =ResponseCacheLocation.Client)]
    public async Task<ActionResult> Index(CancellationToken cancellationToken = default)
    {

        var response = new SuccessResponse
        {
            data = await _mediator.Send(new GetAllCityQuery(), cancellationToken)
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
            data = await _mediator.Send(new GetSpecificCityQuery(id), cancellationToken)
        };

        return Ok(response);

    }



    [HttpGet("[action]/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult> GetResidenceForSpecificCity(Guid id, CancellationToken cancellationToken = default)
    {

        var query = new GetResidenceForSpecificCityQuery(id);

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
    public async Task<ActionResult> Create([FromForm] CityCreateRequest model, CancellationToken cancellationToken = default)
    {
        // show this vedio https://www.youtube.com/watch?v=pDtDEVbEDdQ&list=PL3ewn8T-zRWgO-GAdXjVRh-6thRog6ddg&index=16

        var cityCommand = _mapper.Map<CreateCityCommand>(model);

        var CityResponse = await _mediator.Send(cityCommand, cancellationToken);

        var response = new SuccessResponse
        {
            StatusCode = HttpStatusCode.Created,
            data = CityResponse,
            Message = CityMessages.CityCreated
        };

        return CreatedAtAction(nameof(Details), new { id = CityResponse.Id }, response);

    }






    [HttpPut("[action]/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult> Update(Guid id, [FromForm] CityCreateRequest model, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<UpdateCityCommand>(model,
            opts => opts.AfterMap((src, dest) => dest.Id = id));

        var response = new SuccessResponse
        {
            data = await _mediator.Send(command, cancellationToken),
            Message = CityMessages.CityUpdated
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

        await _mediator.Send(new DeleteCityCommand(id), cancellationToken);

        var response = new SuccessResponse
        {
            Message = CityMessages.CityDeleted
        };

        return Ok(response);

    }





}
