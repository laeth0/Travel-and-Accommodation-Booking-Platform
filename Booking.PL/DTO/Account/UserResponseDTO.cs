




namespace Booking.PL.DTO.Account;
public class UserResponseDTO
{
    [Required]
    public string Id { get; set; } 
    [Required]
    public string FirstName { get; set; } 
    [Required]
    public string LastName { get; set; } 
    [Required]
    public string UserName { get; set; } 
    [Required]
    public string Email { get; set; } 
    [Required]
    public string PhoneNumber { get; set; } 
    public string? ImageName { get; set; } 

}
