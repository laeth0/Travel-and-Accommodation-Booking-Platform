

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected readonly ILogger<BaseController> _logger;
        protected readonly IMediator _mediator;

        public BaseController(IMapper mapper, ILogger<BaseController> logger, IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }

        //for delete action method => mean when we want to delete a category we should delete all products that belong to this category
        //[ProducesResponseType(StatusCodes.Status409Conflict)]// if the resource has any dependencies that would cause a conflict if deleted


    }
}
