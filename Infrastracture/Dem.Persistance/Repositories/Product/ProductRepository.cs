using Dem.Application.Repositories.Product;
using Dem.Persistance.Contexts;

namespace Dem.Persistance.Repositories.Product;

public class ProductRepository(DemBackDbContext dbContext) : Repository<Domain.Entities.Product>(dbContext), IProductRepository
{
}