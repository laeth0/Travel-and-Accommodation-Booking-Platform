using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Interfaces;
public interface IHotelRepository : IRepository<Hotel>, IScopedService
{
}
