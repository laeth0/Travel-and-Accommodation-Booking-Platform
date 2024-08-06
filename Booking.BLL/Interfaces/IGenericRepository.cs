
using System.Linq.Expressions;

namespace Booking.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync(int PageSize = 0, int PageNumber = 0, Expression<Func<T, bool>>? filterCondition = null, List<string>? includeProperty = null);

        Task<T?> GetByIdAsync(Guid id);

        Task<T> AddAsync(T entity);

        T Update(T entity);

        T Delete(T entity);
    }
}
