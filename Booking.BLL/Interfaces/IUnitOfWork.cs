

namespace Booking.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }
        IFlightRepository FlightRepository { get; }
        IResidenceRepository ResidenceRepository { get; }
        IReviewRepository ReviewRepository{ get; }
        IRoomRepository RoomRepository { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
