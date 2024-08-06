


namespace Booking.PL.DTO.Room;
public class RoomResponseDTO
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public int Capacity { get; set; }
    [Required]
    public int Price { get; set; }
    [Required]
    public int Rating { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string ImageName { get; set; }
    [Required]
    public Guid ResidenceId { get; set; }
    [Required]
    public string Residence { get; set; } 


}
