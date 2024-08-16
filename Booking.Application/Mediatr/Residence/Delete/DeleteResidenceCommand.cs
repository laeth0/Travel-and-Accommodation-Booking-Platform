



using MediatR;

namespace Booking.Application.Mediatr;

public class DeleteResidenceCommand(Guid ResidenceId) : IRequest<Unit>
{

    public Guid ResidenceId { get; init; } = ResidenceId;
}
