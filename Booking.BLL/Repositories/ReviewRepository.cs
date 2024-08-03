
using Booking.BLL.Interfaces;
using Booking.DAL.Data;
using Booking.DAL.Entities;


namespace Booking.BLL.Repositories;
public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    public ReviewRepository(ApplicationDbContext Context) : base(Context)
    {
    }
}
