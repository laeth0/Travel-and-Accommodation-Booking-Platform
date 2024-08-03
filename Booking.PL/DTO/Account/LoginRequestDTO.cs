

using System.ComponentModel.DataAnnotations;

namespace Booking.PL.DTO.Account;
public record LoginRequestDTO(

    [Required,EmailAddress]
    string Email,

    [Required]
    string Password
    );

