




namespace Booking.PL.DTO.Account;
public class RolesResponseDTO
{
    [Required]
    public string Id { get; set; } 
    [Required]
    public string Name { get; set; } 
    [Required]
    public string NormalizedName { get; set; } 
    [Required]
    public string ConcurrencyStamp { get; set; } 
}
