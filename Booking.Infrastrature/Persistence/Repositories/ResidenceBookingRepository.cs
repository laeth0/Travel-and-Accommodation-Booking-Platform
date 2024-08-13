





using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Persistence.Repositories;
using Booking.Infrastrature.Data;

namespace Booking.Infrastrature.Persistence.Repositories;
public class ResidenceBookingRepository : Repository<ResidenceBooking>, IResidenceBookingRepository
{
    public ResidenceBookingRepository(ApplicationDbContext Context) : base(Context)
    {

    }
}

