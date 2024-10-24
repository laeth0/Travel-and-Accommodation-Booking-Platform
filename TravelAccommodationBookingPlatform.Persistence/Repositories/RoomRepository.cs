using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;
public class RoomRepository : Repository<Room>, IRoomRepository, IDisposable
{
    private bool _disposed = false;
    public RoomRepository(AppDbContext dbContext) : base(dbContext)
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

    ~RoomRepository()
    {
        Dispose(false);
    }
}
