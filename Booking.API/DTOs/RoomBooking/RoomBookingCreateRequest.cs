
namespace Booking.API.DTOs;

public class RoomBookingCreateRequest
{
    public DateTime CheckInDateUtc { get; set; }
    public DateTime CheckOutDateUtc { get; set; }
    public string UserRemarks { get; set; }
    public Guid RoomId { get; set; }
}
