using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Interfaces;
public interface IReviewRepository : IRepository<Review>, IScopedService
{
}
