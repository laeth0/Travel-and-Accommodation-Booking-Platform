using Booking.Domain.Entities;

namespace Booking.Domain.Interfaces.Services;
public interface IJwtTokenGenerator
{
    Task<(string, DateTime)> GenerateToken(ApplicationUser user);
    string GetValueFromToken(string token, string key = "Roles");
}
