


using MediatR;

namespace Booking.Application.Mediatr;

public class DeleteRoomCommand(Guid RoomId) : IRequest<Unit>
{
    public Guid RoomId { get; init; } = RoomId;
}
