


namespace Booking.API.DTOs;
public class ReviewCreateRequest
{
    public Guid RoomBookingId { get; set; }
    public int Rating { get; set; }
}
