using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Models;
using Booking.Infrastrature.Data;
using Booking.Infrastrature.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;


namespace Booking.Infrastrature.Persistence;
public class Repository<TEntity>(ApplicationDbContext Context) : IRepository<TEntity> where TEntity : class
{

    private readonly ApplicationDbContext _DbContext = Context;

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(
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

        return await query.ToListAsync(cancellationToken) ?? [];
    }

    public async Task<PaginatedList<TEntity>> GetPageAsync(
    int PageSize = 0,
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


        query = query.GetPage(PageNumber, PageSize);

        var items = await query.ToListAsync(cancellationToken) ?? [];

        var pagenationMetadata = await query.GetPaginationMetadataAsync(PageSize, cancellationToken);

        return new PaginatedList<TEntity>(items, pagenationMetadata);
    }


    public async Task<TEntity?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _DbContext.Set<TEntity>().FindAsync(id, cancellationToken);
    }


    public async Task<TEntity?> GetByPrimaryKeysAsync(CancellationToken cancellationToken = default, params object?[]? keyValues)
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
        var entityEntry = _DbContext.Set<TEntity>().Remove(entity);
        return entityEntry.Entity;
    }



    public async Task<int> BulkDeleteAsync(Expression<Func<TEntity, bool>> filterCondition,
        CancellationToken cancellationToken = default
        )
    {
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
    }



}
