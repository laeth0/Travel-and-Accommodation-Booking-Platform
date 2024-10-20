using Microsoft.EntityFrameworkCore.Storage;
using TravelAccommodationBookingPlatform.Application.Interfaces.Persistence.Repositories;

namespace TravelAccommodationBookingPlatform.Application.Interfaces;
public interface IUnitOfWork : IScopedService
{
    IAppUserRepository AppUserRepository { get; }
    IBookingRepository BookingRepository { get; }
    ICityRepository CityRepository { get; }
    ICountryRepository CountryRepository { get; }
    IDiscountRepository DiscountRepository { get; }
    IHotelRepository HotelRepository { get; }
    IImageRepository ImageRepository { get; }
    IPaymentRepository PaymentRepository { get; }
    IReviewRepository ReviewRepository { get; }
    IRoomRepository RoomRepository { get; }
    ITokenRepository TokenRepository { get; }


    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
    IExecutionStrategy CreateExecutionStrategy();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
