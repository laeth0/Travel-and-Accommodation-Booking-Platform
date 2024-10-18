using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class BaseController : ControllerBase
{
    protected readonly IMapper _mapper;
    protected readonly IMediator _mediator;

    protected BaseController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }



}