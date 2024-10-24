using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;
public class HotelRepository : Repository<Hotel>, IHotelRepository
{
    private bool _disposed = false;
    public HotelRepository(AppDbContext dbContext) : base(dbContext)
    {
    }


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

    ~HotelRepository()
    {
        Dispose(false);
    }
}
