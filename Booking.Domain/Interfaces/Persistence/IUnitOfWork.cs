


using Booking.Domain.Interfaces.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Booking.Core.Interfaces.Persistence;
public interface IUnitOfWork
{
    IAmenityRepository AmenityRepository { get; }
    ICityRepository CityRepository { get; }
    ICountryRepository CountryRepository { get; }
    IDiscountRepository DiscountRepository { get; }
    IResidenceOwnerRepository ResidenceOwnerRepository { get; }
    IResidenceRepository ResidenceRepository { get; }
    IResidenceTypeRepository ResidenceTypeRepository { get; }
    IRoomAmenityRepository RoomAmenityRepository { get; }
    IRoomBookingRepository RoomBookingRepository { get; }
    IRoomRepository RoomRepository { get; }
    IRoomTypeRepository RoomTypeRepository { get; }

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
