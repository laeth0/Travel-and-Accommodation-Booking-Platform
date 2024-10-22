using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Models;

namespace TravelAccommodationBookingPlatform.Application.Interfaces;
public interface IJwtTokenGenerator : ITransientService
{
    Task<Jwt> GenerateToken(AppUser user);

}
