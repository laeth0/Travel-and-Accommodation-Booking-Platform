

namespace Booking.PL.DTO.City;
public class CityResponseDTO
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } 
    [Required]
    public string Description { get; set; } 
    [Required]
    public string ImageName { get; set; } 

    [Required]
    public Guid CountryId { get; set; }
    [Required]
    public string Country { get; set; } 
}
