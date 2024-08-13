




using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Persistence.Repositories;
using Booking.Infrastrature.Data;

namespace Booking.Infrastrature.Persistence.Repositories;
public class DiscountRepository : Repository<Discount>, IDiscountRepository
{
    public DiscountRepository(ApplicationDbContext Context) : base(Context)
    {
    }
}
