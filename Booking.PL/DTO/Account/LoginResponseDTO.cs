


namespace Booking.PL.DTO.Account;
public record LoginResponseDTO(

    [Required]
    string UserName,

    [Required]
    string Token,

    [Required]
    DateTime TokenValidTo
    );
