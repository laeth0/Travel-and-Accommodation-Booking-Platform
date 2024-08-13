using Booking.Core.Interfaces.Persistence;
using Booking.Infrastrature.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;


namespace Booking.Infrastrature.Persistence;
public class Repository<TEntity>(ApplicationDbContext Context) : IRepository<TEntity> where TEntity : class
{

    private readonly ApplicationDbContext _DbContext = Context;

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(int PageSize = 0,
        int PageNumber = 0,
        Expression<Func<TEntity, bool>>? filterCondition = null,
        List<string>? includeProperty = null,
        CancellationToken cancellationToken = default
        )
    {
        IQueryable<TEntity> query = _DbContext.Set<TEntity>().AsNoTracking();

        if (filterCondition is { })
            query = query.Where(filterCondition);

        if (includeProperty is { })
        {
            foreach (var include in includeProperty)
                query = query.Include(include);
            query.AsSplitQuery();
        }


        if (PageSize > 0 && PageNumber > 0)
            query = query.Skip((PageNumber - 1) * PageSize).Take(PageSize);


        return await query.ToListAsync(cancellationToken) ?? []; // instead of returning null, return empty list => return await query.ToListAsync() ?? Enumerable.Empty<T>(); 
    }


    public async Task<TEntity?> GetByPrimaryKeysAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _DbContext.Set<TEntity>().FindAsync(id, cancellationToken);

        /*
        Single Key: Use FindAsync(id) when the entity has a single primary key.
        Composite Key: Use FindAsync(new object[] { key1, key2 }) when the entity has a composite primary key.


        FindAsync([id])
            •	Array Syntax: The square brackets [] create an array with a single element, id.
            •	Parameter Type: This syntax is used when FindAsync expects an array of key values. It is useful when the primary key consists of multiple columns.
            •	Usage: This is typically used in scenarios where the entity has a composite key.
        FindAsync(id)
            •	Single Parameter: Here, id is passed directly as a single parameter.
            •	Parameter Type: This is used when FindAsync expects a single key value.
            •	Usage: This is the common usage when the entity has a single-column primary key.
         */
    }


    public async Task<TEntity?> GetByPrimaryKeysAsync(
        CancellationToken cancellationToken = default,
        params object?[]? keyValues
        )
    {
        return await _DbContext.Set<TEntity>().FindAsync(keyValues, cancellationToken);

    }






    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filterCondition, CancellationToken cancellationToken = default)
    {
        return await _DbContext.Set<TEntity>().AnyAsync(filterCondition, cancellationToken);
    }



    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = await _DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        return entityEntry.Entity;
    }



    public TEntity Update(TEntity entity)
    {
        var entityEntry = _DbContext.Set<TEntity>().Update(entity);
        return entityEntry.Entity;
    }




    public TEntity Delete(TEntity entity)
    {
        /*
            Delete Method :-
        •	Single Entity Deletion: This method is designed to delete a single entity.
        •	Entity Tracking: The entity is tracked by the DbContext, which means it will be marked for deletion and the actual deletion will occur when SaveChanges is called.
        •	Performance: Suitable for deleting individual entities. The overhead is minimal for single deletions but can be inefficient if you need to delete many entities in a loop.
         */
        var entityEntry = _DbContext.Set<TEntity>().Remove(entity);
        return entityEntry.Entity;
    }



    public async Task<int> BulkDeleteAsync(Expression<Func<TEntity, bool>> filterCondition,
        CancellationToken cancellationToken = default
        )
    {
        /*
            BulkDeleteAsync Method:-
        •	Bulk Deletion: This method is designed to delete multiple entities that match a given condition.
        •	Direct Database Operation: Uses ExecuteDeleteAsync, which translates to a direct SQL DELETE operation. This is generally more efficient for bulk deletions as it minimizes the overhead of tracking each entity.
        •	Performance: Much faster for deleting multiple entities because it reduces the number of database round-trips and avoids the overhead of entity tracking.
         */
        return await _DbContext.Set<TEntity>().Where(filterCondition).ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }



    public async Task<int> BulkUpdateAsync(
            Expression<Func<TEntity, bool>> filterCondition,
            Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression,
            CancellationToken cancellationToken = default
            )
    {
        return await _DbContext.Set<TEntity>()
            .Where(filterCondition)
            .ExecuteUpdateAsync(updateExpression, cancellationToken);

        /*
         example usage :-

        await repository.BulkUpdateAsync(
            user => !user.IsActive,
            updates => updates
                .SetProperty(user => user.IsActive, true)
                .SetProperty(user => user.LastUpdated, DateTime.UtcNow)
                        );
         */
    }





}
