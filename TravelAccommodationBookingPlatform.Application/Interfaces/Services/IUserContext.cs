using TravelAccommodationBookingPlatform.Domain.Enums;

namespace TravelAccommodationBookingPlatform.Application.Interfaces.Services;
public interface IUserContext : IScopedService
{
    string Id { get; }
    UserRoles Role { get; }
    string Email { get; }
}