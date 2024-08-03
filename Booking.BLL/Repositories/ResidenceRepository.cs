

using Booking.BLL.Interfaces;
using Booking.DAL.Data;
using Booking.DAL.Entities;

namespace Booking.BLL.Repositories;
public class ResidenceRepository : GenericRepository<Residence>, IResidenceRepository
{
    public ResidenceRepository(ApplicationDbContext Context) : base(Context)
    {
    }
}
