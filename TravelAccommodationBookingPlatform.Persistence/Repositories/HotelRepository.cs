using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;
public class HotelRepository : Repository<Hotel>, IHotelRepository
{
    public HotelRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
