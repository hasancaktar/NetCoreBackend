using Dem.Domain.Entities;
using Dem.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Dem.Persistance.Contexts;

public class DemBackDbContext(DbContextOptions options) : IdentityDbContext<User, Role, string>(options)
{
    public virtual DbSet<Product> Products { get; set; }
}