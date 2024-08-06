


using Booking.BLL.Interfaces;
using Booking.DAL.Data;
using Booking.DAL.Entities;

namespace Booking.BLL.Repositories;
public class CountryRepository : GenericRepository<Country>, ICountryRepository
{
    public CountryRepository(ApplicationDbContext Context) : base(Context)
    {
    }
}
