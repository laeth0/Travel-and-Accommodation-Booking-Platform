using Microsoft.EntityFrameworkCore.Storage;

namespace TravelAccommodationBookingPlatform.Application.Interfaces;
public interface IUnitOfWork : IScopedService
{
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
    IExecutionStrategy CreateExecutionStrategy();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
