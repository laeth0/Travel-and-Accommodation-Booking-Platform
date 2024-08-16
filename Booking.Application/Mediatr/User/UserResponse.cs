


namespace Booking.Application.Mediatr;
public class UserResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? ImagePublicId { get; set; }
    public string? ImageUrl { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }

}
