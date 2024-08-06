

using Booking.BLL.Repositories;

namespace Booking.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }
        IFlightRepository FlightRepository { get; }
        IResidenceRepository ResidenceRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IRoomRepository RoomRepository { get; }
        ICountryRepository CountryRepository { get; }
        IRoomBookingRepository RoomBookingRepository { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
