using Dem.Domain.Entities;
using Dem.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dem.Persistance.Contexts;

public class DemBackDbContext : IdentityDbContext<User,Role,string>
{
	public DemBackDbContext(DbContextOptions options):base(options)
	{

	}

	public virtual DbSet<Product> Products { get; set; }
}

