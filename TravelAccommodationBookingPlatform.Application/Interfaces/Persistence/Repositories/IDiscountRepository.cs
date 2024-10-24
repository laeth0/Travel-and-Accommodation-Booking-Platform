using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Interfaces;
public interface IDiscountRepository : IRepository<Discount>, IScopedService
{
}
