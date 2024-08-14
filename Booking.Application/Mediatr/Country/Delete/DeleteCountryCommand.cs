

using MediatR;

namespace Booking.Application.Mediatr;
public class DeleteCountryCommand : IRequest<Unit>
{

    public Guid CountryId { get; init; }


    public DeleteCountryCommand(Guid countryId)
    {
        CountryId = countryId;
    }

}