using Dem.Domain.Entities.Common;
using System.Linq.Expressions;

namespace Dem.Application.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAll(bool tracking = true);
    IQueryable<T> GetWhere(Expression<Func<T, bool>> filter, bool tracking = true);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, bool tracking = true);
    Task<T> GetByIdAsync(string id, bool tracking = true);
    Task<bool> AddAsync(T entity);
    Task<bool> AddRangeAsync(List<T> entities);
    bool Remove(T entity);
    bool RemoveRange(List<T> entities);
    Task<bool> RemoveAsync(string id);
    bool Update(T entity);

    Task<int> SaveAsync();
}

