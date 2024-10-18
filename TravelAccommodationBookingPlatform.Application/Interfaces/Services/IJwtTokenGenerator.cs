using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Interfaces;
public interface IJwtTokenGenerator : IScopedService
{
    Task<(string, DateTime)> GenerateToken(AppUser user);

}
