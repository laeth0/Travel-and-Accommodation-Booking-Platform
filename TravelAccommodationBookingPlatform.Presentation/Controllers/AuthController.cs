using AutoMapper;
using MediatR;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers;
public class AuthController : BaseController
{

    public AuthController(IMapper mapper, IMediator mediator) : base(mapper, mediator)
    { }



}
