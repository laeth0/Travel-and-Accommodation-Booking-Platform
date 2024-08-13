




using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Persistence.Repositories;
using Booking.Infrastrature.Data;

namespace Booking.Infrastrature.Persistence.Repositories;
public class RoomTypeRepository : Repository<RoomType>, IRoomTypeRepository
{
    public RoomTypeRepository(ApplicationDbContext Context) : base(Context)
    {
    }
}