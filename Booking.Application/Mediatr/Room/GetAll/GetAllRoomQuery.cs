


using MediatR;

namespace Booking.Application.Mediatr;
public class GetAllRoomQuery : IRequest<IReadOnlyList<RoomResponse>>
{
}
