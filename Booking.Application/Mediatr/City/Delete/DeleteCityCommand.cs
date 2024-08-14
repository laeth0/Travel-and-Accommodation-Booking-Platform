

using MediatR;

namespace Booking.Application.Mediatr;
public class DeleteCityCommand(Guid cityId) : IRequest<Unit>
{

    public Guid CityId { get; init; } = cityId;
}