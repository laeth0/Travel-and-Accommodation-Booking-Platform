

namespace Booking.PL.DTO.RoomBooking;
public class RoomBookingResponseDTO
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CheckIn { get; set; }
    [Required]
    public DateTime CheckOut { get; set; }
    [Required]
    public int TotalPrice { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public DateTime UpdatedAt { get; set; }
    [Required]

    public Guid RoomId { get; set; }
    [Required]

    public string GuestId { get; set; } 
    [Required]
    public string Guest { get; set; } 

}
