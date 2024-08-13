




using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Persistence.Repositories;
using Booking.Infrastrature.Data;

namespace Booking.Infrastrature.Persistence.Repositories;
public class ResidenceRepository : Repository<Residence>, IResidenceRepository
{
    public ResidenceRepository(ApplicationDbContext Context) : base(Context)
    {
    }
}
