




using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Persistence.Repositories;
using Booking.Infrastrature.Data;

namespace Booking.Infrastrature.Persistence.Repositories;
public class RoomAmenityRepository : Repository<RoomAmenity>, IRoomAmenityRepository
{
    public RoomAmenityRepository(ApplicationDbContext Context) : base(Context)
    {
    }
}
