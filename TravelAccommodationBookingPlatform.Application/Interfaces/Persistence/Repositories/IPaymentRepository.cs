using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Interfaces;
public interface IPaymentRepository : IRepository<Payment>, IScopedService
{
}
