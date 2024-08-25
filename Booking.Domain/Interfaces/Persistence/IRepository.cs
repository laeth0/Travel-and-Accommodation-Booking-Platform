using Booking.Domain.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Booking.Core.Interfaces.Persistence;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IReadOnlyList<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? filterCondition = null,
            List<string>? includeProperty = null,
            CancellationToken cancellationToken = default
            );

    Task<PaginatedList<TEntity>> GetPageAsync(
       int PageSize = 0,
       int PageNumber = 0,
       Expression<Func<TEntity, bool>>? filterCondition = null,
       List<string>? includeProperty = null,
       CancellationToken cancellationToken = default
       );

    Task<TEntity?> FindAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByPrimaryKeysAsync(
           CancellationToken cancellationToken = default,
           params object?[]? keyValues
           );

    Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filterCondition, CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    TEntity Update(TEntity entity);

    TEntity Delete(TEntity entity);

    Task<int> BulkDeleteAsync(Expression<Func<TEntity, bool>> filterCondition, CancellationToken cancellationToken = default);

    Task<int> BulkUpdateAsync(
            Expression<Func<TEntity, bool>> filterCondition,
            Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression,
            CancellationToken cancellationToken = default
            );

}
