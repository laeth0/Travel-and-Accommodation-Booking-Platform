using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;
public class DiscountRepository : Repository<Discount>, IDiscountRepository
{
    private bool _disposed = false;
    public DiscountRepository(AppDbContext dbContext) : base(dbContext)
    { }


    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _dbContext?.Dispose();
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~DiscountRepository()
    {
        Dispose(false);
    }
}
