

namespace Booking.PL.DTO.Account;
public class UpdateProfileRequestDTO
{
    [Required]
    public string FirstName { get; set; } 
    [Required]
    public string LastName { get; set; } 
    public IFormFile? Image { get; set; } 
}
