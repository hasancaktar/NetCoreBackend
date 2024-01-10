using Dem.Application.Repositories;
using Dem.Domain.Entities.Common;
using Dem.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Dem.Persistance.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly DemBackDbContext _dbContext;
    public Repository(DemBackDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public DbSet<T> Table => _dbContext.Set<T>();

    public IQueryable<T> GetAll(bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }
    public IQueryable<T> GetWhere(Expression<Func<T, bool>> filter, bool tracking = true)
    {
        var query = Table.Where(filter);
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }
    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = Table.AsNoTracking();
        return await query.FirstOrDefaultAsync(filter);
    }
    public async Task<T> GetByIdAsync(string id, bool tracking = true)
    //=> await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
    //=> await Table.FindAsync(Guid.Parse(id));
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = Table.AsNoTracking();
        return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
    }
    public async Task<bool> AddAsync(T entity)
    {
        EntityEntry<T> entityEntry = await Table.AddAsync(entity);
        return entityEntry.State == EntityState.Added;
    }
    public async Task<bool> AddRangeAsync(List<T> entities)
    {
        await Table.AddRangeAsync(entities);
        return true;
    }
    public bool Remove(T entity)
    {
        EntityEntry<T> entityEntry = Table.Remove(entity);
        return entityEntry.State == EntityState.Deleted;
    }
    public bool RemoveRange(List<T> entities)
    {
        Table.RemoveRange(entities);
        return true;
    }
    public async Task<bool> RemoveAsync(string id)
    {
        T entity = await Table.FirstOrDefaultAsync(entity => entity.Id == Guid.Parse(id));
        return Remove(entity);
    }
    public bool Update(T entity)
    {
        EntityEntry entityEntry = Table.Update(entity);
        return entityEntry.State == EntityState.Modified;
    }
    public async Task<int> SaveAsync()
        => await _dbContext.SaveChangesAsync();


}

