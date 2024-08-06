



namespace Booking.PL.DTO.RoomBooking;
public class RoomBookingUpdateRequestDTO
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CheckIn { get; set; }
    [Required]
    public DateTime CheckOut { get; set; }
}
