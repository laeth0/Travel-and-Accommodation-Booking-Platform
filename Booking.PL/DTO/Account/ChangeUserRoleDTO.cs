namespace Booking.PL.DTO.Account
{
    public class ChangeUserRoleDTO
    {
        [Required]
        public string UserId { get; set; } 

        [Required]
        public string RoleName { get; set; } 
    }
}
