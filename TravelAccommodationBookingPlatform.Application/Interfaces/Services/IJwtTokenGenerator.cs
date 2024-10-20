using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Interfaces;
public interface IJwtTokenGenerator : ITransientService
{
    Task<(string, DateTime)> GenerateToken(AppUser user);

}
