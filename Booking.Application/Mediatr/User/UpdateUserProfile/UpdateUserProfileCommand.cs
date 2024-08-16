


using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Mediatr;

public class UpdateUserProfileCommand : IRequest<UserResponse>
{
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public IFormFile? Image { get; set; }
    public string PhoneNumber { get; set; }
}

