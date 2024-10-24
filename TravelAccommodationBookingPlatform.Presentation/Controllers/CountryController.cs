using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Features.Country.Create;
using TravelAccommodationBookingPlatform.Application.Shared.Extensions;
using TravelAccommodationBookingPlatform.Presentation.Attributes;
using TravelAccommodationBookingPlatform.Presentation.Shared;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers;
public class CountryController : BaseController
{
    public CountryController(IMapper mapper, IMediator mediator) : base(mapper, mediator)
    {
    }

    [HttpPost("[action]")]
    [ProducesResponseType(typeof(CountryResponse), StatusCodes.Status201Created)]
    [ProducesError(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateCountry(CountryCreateCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblemDetails();
    }
}
