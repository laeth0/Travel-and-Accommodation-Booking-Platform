


using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Persistence.Repositories;
using Booking.Infrastrature.Data;

namespace Booking.Infrastrature.Persistence.Repositories;
public class RoomRepository : Repository<Room>, IRoomRepository
{
    public RoomRepository(ApplicationDbContext Context) : base(Context)
    {
    }
}
