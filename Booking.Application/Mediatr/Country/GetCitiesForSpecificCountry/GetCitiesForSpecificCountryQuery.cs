


using MediatR;

namespace Booking.Application.Mediatr;

public class GetCitiesForSpecificCountryQuery(Guid CountryId) : IRequest<IReadOnlyList<CityResponse>>
{
    public Guid CountryId { get; set; } = CountryId;
}

