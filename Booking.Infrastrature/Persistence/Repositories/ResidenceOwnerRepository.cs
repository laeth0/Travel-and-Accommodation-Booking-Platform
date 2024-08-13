



using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Persistence.Repositories;
using Booking.Infrastrature.Data;

namespace Booking.Infrastrature.Persistence.Repositories;
public class ResidenceOwnerRepository : Repository<ResidenceOwner>, IResidenceOwnerRepository
{
    public ResidenceOwnerRepository(ApplicationDbContext Context) : base(Context)
    {
    }
}
