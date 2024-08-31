


using AutoMapper;
using MediatR;

namespace Booking.API.Controllers;

public class RoleController : BaseController
{
    public RoleController(IMapper mapper, ILogger<BaseController> logger, IMediator mediator)
     : base(mapper, logger, mediator)
    {
    }

}
