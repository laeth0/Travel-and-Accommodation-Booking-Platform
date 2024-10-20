using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TravelAccommodationBookingPlatform.Presentation.Constants;
using TravelAccommodationBookingPlatform.Presentation.Contracts;

namespace TravelAccommodationBookingPlatform.Presentation.Shared;

[ApiController]
[Route("[controller]")]
[EnableRateLimiting(PresentationRules.RateLimitPolicies.Fixed)]
public abstract class BaseController : ControllerBase
{
    protected readonly IMapper _mapper;
    protected readonly IMediator _mediator;

    protected BaseController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }


    /// <summary>
    /// Returns an OK response with the specified value and message.
    /// </summary>
    /// <param name="value">The value to include in the response.</param>
    /// <param name="message">The message to include in the response.</param>
    /// <returns>An OK response.</returns>
    protected IActionResult Ok(object value, string? message) =>
        base.Ok(
            new ApiResponse
            {
                Data = value,
                Message = message ?? "data fetched successfully",
                StatusCode = HttpStatusCode.OK
            }
        );



}