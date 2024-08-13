



using MediatR;

namespace Booking.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly ILogger<BaseController> _logger;
        protected readonly IMediator _mediator;

        public BaseController(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<BaseController> logger,
            IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }

        //for delete action method => mean when we want to delete a category we should delete all products that belong to this category
        //[ProducesResponseType(StatusCodes.Status409Conflict)]// if the resource has any dependencies that would cause a conflict if deleted


    }
}
