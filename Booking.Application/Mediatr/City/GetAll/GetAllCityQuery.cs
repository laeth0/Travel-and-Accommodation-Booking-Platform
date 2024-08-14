



using MediatR;

namespace Booking.Application.Mediatr;
public class GetAllCityQuery : IRequest<IReadOnlyList<CityResponse>>
{
}
