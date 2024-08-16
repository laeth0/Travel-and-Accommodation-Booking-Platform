



using MediatR;

namespace Booking.Application.Mediatr;

public class GetRoomForSpecificResidenceQuery(Guid id) : IRequest<IReadOnlyList<RoomResponse>>
{
    public Guid Id { get; set; } = id;

}
