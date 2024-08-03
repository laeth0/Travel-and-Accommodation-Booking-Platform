


namespace Booking.PL.DTO.Account;
public class ResetPasswordRequestDTO
{
    [EmailAddress, Required]
    public string Email { get; set; } = null!;

    [Required]
    public string NewPassword { get; set; } = null!;


    [Required, Compare(nameof(NewPassword))]
    public string ConfirmNewPassword { get; set; } = null!;
}