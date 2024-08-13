





using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Persistence.Repositories;
using Booking.Infrastrature.Data;


namespace Booking.Infrastrature.Persistence.Repositories;
internal class AmenityRepository : Repository<Amenity>, IAmenityRepository
{
    public AmenityRepository(ApplicationDbContext Context) : base(Context)
    {
    }
}
