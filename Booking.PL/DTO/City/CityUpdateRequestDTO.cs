


namespace Booking.PL.DTO.City;
public class CityUpdateRequestDTO
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } 
    [Required]
    public string Description { get; set; } 
    [Required]
    public IFormFile Image { get; set; } 
    [Required]

    public Guid CountryId { get; set; }
}
