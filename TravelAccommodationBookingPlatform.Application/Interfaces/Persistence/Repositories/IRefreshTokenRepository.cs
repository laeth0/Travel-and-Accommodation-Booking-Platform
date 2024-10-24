using System.Security.Claims;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared.MaybePattern;

namespace TravelAccommodationBookingPlatform.Application.Interfaces.Persistence.Repositories;
public interface IRefreshTokenRepository : IRepository<RefreshToken>, IScopedService
{
    string GenerateRefreshToken();
    Maybe<ClaimsPrincipal> GetPrincipalFromToken(string token);

}
