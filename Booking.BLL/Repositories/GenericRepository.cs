
using Booking.BLL.Interfaces;
using Booking.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Booking.BLL.Repositories
{
    public class GenericRepository<T>(ApplicationDbContext Context) : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _DbContext = Context;

        public async Task<IReadOnlyList<T>> GetAllAsync(int PageSize = 0, int PageNumber = 0, Expression<Func<T, bool>>? filterCondition = null, List<string>? includeProperty = null)
        {
            IQueryable<T> query = _DbContext.Set<T>().AsNoTracking();

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


            return await query.ToListAsync() ?? []; // instead of returning null, return empty list => return await query.ToListAsync() ?? Enumerable.Empty<T>(); 
        }


        public async Task<T?> GetByIdAsync(Guid id) => await _DbContext.Set<T>().FindAsync(id);


        public async Task<T> AddAsync(T entity)
        {
            var entityEntry = await _DbContext.Set<T>().AddAsync(entity);
            return entityEntry.Entity;
        }

        public T Update(T entity)
        {
            var entityEntry = _DbContext.Set<T>().Update(entity);
            return entityEntry.Entity;
        }

        public T Delete(T entity)
        {
            var entityEntry = _DbContext.Set<T>().Remove(entity);
            return entityEntry.Entity;
            //_DbContext.Set<T>().ExecuteDelete(entity); // bulk delete
        }

    }
}
