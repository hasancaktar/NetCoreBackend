using Dem.Persistance.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Dem.Persistance.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DemBackDbContext _dbContext;
    private readonly IDbContextTransaction _transaction;

    public UnitOfWork(DemBackDbContext dbContext, IDbContextTransaction transaction)
    {
        _dbContext = dbContext;
        _transaction = transaction;
    }

    public async Task CommitAsync()
    {
        try
        {
            await _transaction.CommitAsync();
        }
        catch (Exception exception)
        {
            await _transaction.RollbackAsync();
            throw exception;
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