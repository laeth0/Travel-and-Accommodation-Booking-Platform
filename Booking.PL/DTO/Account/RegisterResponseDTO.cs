

using System.ComponentModel.DataAnnotations;

namespace Booking.PL.DTO.Account;
public record RegisterResponseDTO(
    [Required]
    string UserName,

    [EmailAddress,Required]
    string Email,

    [Required]
    string PhoneNumber
    );
