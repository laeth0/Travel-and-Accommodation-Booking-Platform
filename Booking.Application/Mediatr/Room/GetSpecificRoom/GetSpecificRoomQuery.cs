

using MediatR;

namespace Booking.Application.Mediatr;
public class GetSpecificRoomQuery(Guid Id) : IRequest<RoomResponse>
{
    public Guid Id { get; set; } = Id;

}
