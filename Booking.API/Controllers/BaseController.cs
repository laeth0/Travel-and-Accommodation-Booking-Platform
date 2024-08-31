

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

    }
}
