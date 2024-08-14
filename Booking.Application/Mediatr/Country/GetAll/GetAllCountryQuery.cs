



using MediatR;

namespace Booking.Application.Mediatr;
public class GetAllCountryQuery : IRequest<IReadOnlyList<CountryResponse>>
{
}
