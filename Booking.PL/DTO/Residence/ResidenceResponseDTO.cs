


namespace Booking.PL.DTO.Residence;
public class ResidenceResponseDTO
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } 
    [Required]
    public string Description { get; set; } 
    [Required]
    public string Address { get; set; } 
    [Required]
    public string ImageName { get; set; } 
    [Required]
    public string PhoneNumber { get; set; } 
    [Required]
    public string Email { get; set; } 
    [Required]
    public int FloorsNumber { get; set; }
    [Required]
    public string Type { get; set; } 
    [Required]

    public Guid CityId { get; set; }
    [Required]
    public string City { get; set; } 
    [Required]
    public string OwnerId { get; set; } 
    [Required]
    public string Owner { get; set; } 

}
