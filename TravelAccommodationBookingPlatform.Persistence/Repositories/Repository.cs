using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Domain.Shared.MaybePattern;
using TravelAccommodationBookingPlatform.Domain.Specifications;
using TravelAccommodationBookingPlatform.Persistence.Specifications;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext) => _dbContext = dbContext;


    public async Task<Maybe<TEntity>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => await _dbContext.Set<TEntity>().FindAsync(id, cancellationToken)
        ?? Maybe<TEntity>.None;


    public async Task<Maybe<TEntity>> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    => await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken)
        ?? Maybe<TEntity>.None;


    public async Task<Maybe<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    => await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken)
        ?? Maybe<TEntity>.None;


    public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    => await ApplySpecification(specification).ToListAsync(cancellationToken);


    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => await _dbContext.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);


    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    => await _dbContext.Set<TEntity>().ToListAsync(cancellationToken);


    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        var entityEntry = await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        return entityEntry.Entity;
    }


    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        => await _dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);


    public TEntity Delete(TEntity entity)
    {
        var entityEntry = _dbContext.Set<TEntity>().Remove(entity);
        return entityEntry.Entity;
    }


    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbContext.Set<TEntity>().RemoveRange(entities);
    }


    public async Task<int> BulkDeleteAsync(Expression<Func<TEntity, bool>> filterCondition, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>()
                        .Where(filterCondition)
                        .ExecuteDeleteAsync(cancellationToken);
    }


    public TEntity Update(TEntity entity)
    {
        var entityEntry = _dbContext.Set<TEntity>().Update(entity);
        return entityEntry.Entity;
    }


    public async Task<int> BulkUpdateAsync(
            Expression<Func<TEntity, bool>> filterCondition,
            Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression,
            CancellationToken cancellationToken = default
            )
    {
        return await _dbContext.Set<TEntity>()
            .Where(filterCondition)
            .ExecuteUpdateAsync(updateExpression, cancellationToken);
    }



    public async Task<bool> AnyAsync(ISpecification<TEntity> specification)
        => await CountAsync(specification) > 0;


    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        => await CountAsync(predicate) > 0;


    public async Task<int> CountAsync(ISpecification<TEntity> specification)
        => await ApplySpecification(specification).CountAsync();


    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        => await _dbContext.Set<TEntity>().CountAsync(predicate);


    protected IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
    {
        var query = _dbContext.Set<TEntity>().AsQueryable();

        return SpecificationEvaluator<TEntity>.GetQuery(query, specification);
    }
}
