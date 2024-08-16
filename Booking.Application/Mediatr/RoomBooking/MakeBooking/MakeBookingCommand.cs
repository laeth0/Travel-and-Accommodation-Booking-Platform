



using MediatR;

namespace Booking.Application.Mediatr;

public class MakeBookingCommand : IRequest<RoomBookingResponse>
{
    public DateTime CheckInDateUtc { get; set; }
    public DateTime CheckOutDateUtc { get; set; }
    public string UserRemarks { get; set; }
    public Guid RoomId { get; set; }
    public string UserId { get; set; }

}
