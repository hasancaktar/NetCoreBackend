using Dem.Application.Abstraction;
using Dem.Persistance.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Dem.Persistance.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DemBackDbContext _dbContext;
    private readonly IDbContextTransaction _transaction;

    public UnitOfWork(DemBackDbContext dbContext)
    {
        _dbContext = dbContext;
        _transaction = _dbContext.Database.BeginTransaction();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _transaction.CommitAsync();
        }
        catch (Exception)
        {
            await _transaction.RollbackAsync();
            throw;
        }
        finally
        {
            Dispose();
        }
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}