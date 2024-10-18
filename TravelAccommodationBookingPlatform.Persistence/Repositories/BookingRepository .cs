using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;
public class BookingRepository : Repository<Booking>, IBookingRepository
{
    public BookingRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}

