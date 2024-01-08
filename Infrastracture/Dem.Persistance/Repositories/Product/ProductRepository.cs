using Dem.Application.Repositories.Product;
using Dem.Persistance.Contexts;

namespace Dem.Persistance.Repositories.Product;

public class ProductRepository : Repository<Domain.Entities.Product>, IProductRepository
{
    public ProductRepository(DemBackDbContext dbContext) : base(dbContext)
    {
    }
}

