




using MediatR;

namespace Booking.Application.Mediatr;

public class GetSpecificCountryQuery(Guid Id) : IRequest<CountryResponse>
{
    public Guid Id { get; set; } = Id;
}
