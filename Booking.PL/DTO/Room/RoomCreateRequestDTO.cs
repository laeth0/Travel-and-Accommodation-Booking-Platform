


namespace Booking.PL.DTO.Room;
public class RoomCreateRequestDTO
{
    [Required]
    public int Capacity { get; set; }
    [Required]
    public int Price { get; set; }
    [Required]
    public int Rating { get; set; }
    [Required]
    public string Description { get; set; } 
    [Required]
    public IFormFile Image { get; set; } 
    [Required]
    public Guid ResidenceId { get; set; }
}
