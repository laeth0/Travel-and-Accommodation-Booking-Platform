


namespace Booking.PL.DTO.RoomBooking;
public class RoomBookingCreateRequestDTO
{
    [Required]
    public DateTime CheckIn { get; set; }
    [Required]
    public DateTime CheckOut { get; set; }
    [Required]
    public Guid RoomId { get; set; }
}
