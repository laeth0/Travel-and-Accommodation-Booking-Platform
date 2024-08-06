


namespace Booking.PL.DTO.Account;
public class ResetPasswordRequestDTO
{
    [EmailAddress, Required]
    public string Email { get; set; } 

    [Required]
    public string NewPassword { get; set; } 


    [Required, Compare(nameof(NewPassword))]
    public string ConfirmNewPassword { get; set; } 
}