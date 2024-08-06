


namespace Booking.PL.DTO.Residence;
public class ResidenceCreateRequestDTO
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public IFormFile Image { get; set; } 
    [Phone]
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public int FloorsNumber { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    public Guid CityId { get; set; }
    [Required]
    public string OwnerId { get; set; } 
}
