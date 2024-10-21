using AutoMapper;
using MediatR;
using TravelAccommodationBookingPlatform.Presentation.Shared;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers;
public class CityController : BaseController
{
    public CityController(IMapper mapper, IMediator mediator) : base(mapper, mediator)
    { }

}
