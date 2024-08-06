


using Booking.BLL.Interfaces;
using Booking.DAL.Data;
using Booking.DAL.Entities;

namespace Booking.BLL.Repositories;
public class RoomBookingRepository : GenericRepository<RoomBooking>, IRoomBookingRepository
{
    public RoomBookingRepository(ApplicationDbContext Context) : base(Context)
    {
    }
}
