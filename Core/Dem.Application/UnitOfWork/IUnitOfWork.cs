namespace Dem.Persistance.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task CommitAsync();
}