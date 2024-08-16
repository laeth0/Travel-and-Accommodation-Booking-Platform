

namespace Booking.API.DTOs;

public class UpdateProfileRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public IFormFile? Image { get; set; }
    public string PhoneNumber { get; set; }
}
