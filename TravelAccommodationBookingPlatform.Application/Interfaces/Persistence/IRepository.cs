using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using TravelAccommodationBookingPlatform.Domain.Shared.MaybePattern;
using TravelAccommodationBookingPlatform.Domain.Specifications;

namespace TravelAccommodationBookingPlatform.Application.Interfaces;
public interface IRepository<TEntity> where TEntity : class
{

    Task<Maybe<TEntity>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Maybe<TEntity>> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

    Task<Maybe<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    TEntity Delete(TEntity entity);

    void DeleteRange(IEnumerable<TEntity> entities);

    Task<int> BulkDeleteAsync(Expression<Func<TEntity, bool>> filterCondition, CancellationToken cancellationToken = default);

    TEntity Update(TEntity entity);

    Task<int> BulkUpdateAsync(
            Expression<Func<TEntity, bool>> filterCondition,
            Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression,
            CancellationToken cancellationToken = default
            );

    Task<bool> AnyAsync(ISpecification<TEntity> specification);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    Task<int> CountAsync(ISpecification<TEntity> specification);

    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
}
