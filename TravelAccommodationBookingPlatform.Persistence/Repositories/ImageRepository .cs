using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;
public class ImageRepository : Repository<Image>, IImageRepository
{
    public ImageRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
