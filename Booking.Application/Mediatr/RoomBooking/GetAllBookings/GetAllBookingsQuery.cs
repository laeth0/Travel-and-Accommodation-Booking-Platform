



using MediatR;

namespace Booking.Application.Mediatr;
public class GetAllBookingsQuery(string userId) : IRequest<IReadOnlyList<RoomBookingResponse>>
{
    public string UserId { get; set; } = userId;
}
