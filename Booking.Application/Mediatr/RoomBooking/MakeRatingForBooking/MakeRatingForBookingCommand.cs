using MediatR;

namespace Booking.Application.Mediatr;
public class MakeRatingForBookingCommand(Guid RoomBookingId, int Rating) : IRequest<RoomBookingResponse>
{
    public Guid RoomBookingId { get; set; } = RoomBookingId;
    public int Rating { get; set; } = Rating;
}
