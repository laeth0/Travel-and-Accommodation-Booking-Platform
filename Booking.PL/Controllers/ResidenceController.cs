

namespace Booking.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidenceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private readonly ILogger<ResidenceController> _logger;

        public ResidenceController(
            IUnitOfWork unitOfWork,
            IServiceManager serviceManager,
            IMapper mapper,
            ILogger<ResidenceController> logger
            )
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


    }
}
