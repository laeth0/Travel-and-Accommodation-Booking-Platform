using MediatR;

namespace Booking.Application.Mediatr;
public class GetUserProfileQuery(string id) : IRequest<UserResponse>
{
    public string UserId { get; set; } = id;
}
