using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Models;

namespace TravelAccommodationBookingPlatform.Application.Interfaces;
public interface IJwtTokenGenerator : ITransientService
{
    Task<JwtAccessToken> GenerateToken(AppUser user);

}
