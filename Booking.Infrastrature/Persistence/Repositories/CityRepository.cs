




using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Persistence.Repositories;
using Booking.Infrastrature.Data;

namespace Booking.Infrastrature.Persistence.Repositories;
public class CityRepository : Repository<City>, ICityRepository
{
    public CityRepository(ApplicationDbContext Context) : base(Context)
    {

    }
}
