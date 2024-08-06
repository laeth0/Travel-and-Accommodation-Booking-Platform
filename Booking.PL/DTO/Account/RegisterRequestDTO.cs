

namespace Booking.PL.DTO.Account;
public class RegisterRequestDTO
{
    [Required, CapitalizeCheck(ErrorMessage = "The First Name must start with capital letter")]
    public string FirstName { get; set; } 

    [Required, CapitalizeCheck(ErrorMessage = "The Last Name must start with capital letter")]
    public string LastName { get; set; } 

    [Required, EmailAddress]
    public string Email { get; set; } 

    [Required]
    public string Password { get; set; } 


    [Required, Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } 


    [Required, Phone]
    public string PhoneNumber { get; set; } 

}
