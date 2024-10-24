using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Interfaces;
public interface IRoomRepository : IRepository<Room>, IScopedService
{
}
