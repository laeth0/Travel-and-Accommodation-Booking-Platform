using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;
public class CountryRepository : Repository<Country>, ICountryRepository
{
    public CountryRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
