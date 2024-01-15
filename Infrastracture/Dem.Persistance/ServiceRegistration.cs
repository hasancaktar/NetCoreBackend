using Dem.Domain.Entities.Identity;
using Dem.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Dem.Application.Repositories.Product;
using Dem.Persistance.Repositories.Product;
using Microsoft.Extensions.Configuration;
using Dem.Application.Abstraction;

namespace Dem.Persistance;

public static class ServiceRegistration
{
    public static void AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DemBackDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MSSQL")));
        services.AddIdentityCore<User>(options =>
        {
            // IdentityCore yapılandırma seçenekleri
        }).AddRoles<Role>()
        .AddEntityFrameworkStores<DemBackDbContext>()
        .AddSignInManager<SignInManager<User>>()
        .AddDefaultTokenProviders();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
    }
}