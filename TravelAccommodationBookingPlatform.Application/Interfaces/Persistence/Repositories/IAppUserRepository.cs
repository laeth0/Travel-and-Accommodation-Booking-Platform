using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Interfaces;
public interface IAppUserRepository : IRepository<AppUser>, IScopedService
{
}
