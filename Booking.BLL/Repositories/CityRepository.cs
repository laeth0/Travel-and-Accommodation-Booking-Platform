

using Booking.BLL.Interfaces;
using Booking.DAL.Data;
using Booking.DAL.Entities;

namespace Booking.BLL.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext Context) : base(Context)
        {

        }
    }
}
