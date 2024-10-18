using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;
public class CityRepository : Repository<City>, ICityRepository
{
    public CityRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
