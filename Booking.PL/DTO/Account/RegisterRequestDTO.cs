

namespace Booking.PL.DTO.Account;
public class RegisterRequestDTO
{
    [Required, CapitalizeCheck(ErrorMessage = "The First Name must start with capital letter")]
    public string FirstName { get; set; } = null!;

    [Required, CapitalizeCheck(ErrorMessage = "The Last Name must start with capital letter")]
    public string LastName { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;


    [Required, Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;


    [Required, Phone]
    public string PhoneNumber { get; set; } = null!;

}
