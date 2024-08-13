






using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Persistence.Repositories;
using Booking.Infrastrature.Data;

namespace Booking.Infrastrature.Persistence.Repositories;
public class CountryRepository : Repository<Country>, ICountryRepository
{
    public CountryRepository(ApplicationDbContext Context) : base(Context)
    {
    }
}
