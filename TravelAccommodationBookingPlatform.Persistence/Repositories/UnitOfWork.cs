using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TravelAccommodationBookingPlatform.Application.Interfaces;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;
public class UnitOfWork : IUnitOfWork, IDisposable
{

    private bool _disposed = false;
    private readonly AppDbContext _dbContext;


    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);
    }



    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContext.Database.CurrentTransaction is null) return;

        await _dbContext.Database.CommitTransactionAsync(cancellationToken);
    }


    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContext.Database.CurrentTransaction is null) return;

        await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
    }

    public IExecutionStrategy CreateExecutionStrategy()
    {
        return _dbContext.Database.CreateExecutionStrategy();
    }


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
         await _dbContext.SaveChangesAsync(cancellationToken);



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

    ~UnitOfWork()
    {
        Dispose(false);
    }

}
