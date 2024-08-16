



using MediatR;

namespace Booking.Application.Mediatr;
public class GetSpecificResidenceQuery(Guid Id) : IRequest<ResidenceResponse>
{
    public Guid Id { get; set; } = Id;
}
