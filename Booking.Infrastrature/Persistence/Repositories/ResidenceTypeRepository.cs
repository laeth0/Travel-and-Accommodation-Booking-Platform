




using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Persistence.Repositories;
using Booking.Infrastrature.Data;

namespace Booking.Infrastrature.Persistence.Repositories;
public class ResidenceTypeRepository : Repository<ResidenceType>, IResidenceTypeRepository
{
    public ResidenceTypeRepository(ApplicationDbContext Context) : base(Context)
    {
    }
}
