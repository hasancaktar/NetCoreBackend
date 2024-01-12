namespace Dem.Application.Abstraction;

public interface IUnitOfWork : IDisposable
{
    Task CommitAsync();
}