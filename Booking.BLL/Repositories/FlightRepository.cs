

using Booking.BLL.Interfaces;
using Booking.DAL.Data;
using Booking.DAL.Entities;

namespace Booking.BLL.Repositories;

public class FlightRepository : GenericRepository<Flight>, IFlightRepository
{
    public FlightRepository(ApplicationDbContext Context) : base(Context)
    {
    }
}
