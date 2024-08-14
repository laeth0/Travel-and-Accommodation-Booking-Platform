




using MediatR;

namespace Booking.Application.Mediatr;

public class GetSpecificCityQuery(Guid Id) : IRequest<CityResponse>
{
    public Guid Id { get; set; } = Id;
}
