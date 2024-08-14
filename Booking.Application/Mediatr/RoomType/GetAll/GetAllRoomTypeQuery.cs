

using MediatR;

namespace Booking.Application.Mediatr;
public class GetAllRoomTypeQuery : IRequest<IReadOnlyList<RoomTypeResponse>>
{
}
