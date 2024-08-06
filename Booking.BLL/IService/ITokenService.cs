


using Booking.DAL.Entities;

namespace Booking.BLL.IService
{
    public interface ITokenService
    {
        Task<(string, DateTime)> GenerateToken(ApplicationUser user);
        string GetValueFromToken(string token, string key="Roles");
    }
}
