



using AutoMapper;
using Booking.API.CustomizeResponses;
using Booking.Application.Mediatr;
using Booking.Domain.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{

    public class AmenityController : BaseController
    {

        public AmenityController(IMapper mapper, ILogger<BaseController> logger, IMediator mediator, ICloudinaryService cloudinaryService)
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
                data = await _mediator.Send(new GetAllAmenityQuery(), cancellationToken)
            };

            return Ok(response);

        }


    }
}
