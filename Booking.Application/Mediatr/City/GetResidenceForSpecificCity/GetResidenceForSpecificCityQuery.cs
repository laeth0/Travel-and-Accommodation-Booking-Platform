



using MediatR;

namespace Booking.Application.Mediatr;

public class GetResidenceForSpecificCityQuery(Guid CityId) : IRequest<IReadOnlyList<ResidenceResponse>>
{
    public Guid CityId { get; set; } = CityId;
}
