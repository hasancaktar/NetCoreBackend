using Dem.Domain.Entities.Identity;
using Dem.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;


namespace Dem.Persistance;

public static class ServiceRegistration
{
    public static void AddPersistanceServices(this IServiceCollection services)
    {
        services.AddDbContext<DemBackDbContext>(options => options.UseSqlServer(@"Server=(LocalDB)\.;Database=DemBack;Trusted_Connection=True;"));
        services.AddIdentityCore<User>(options =>
        {
            // IdentityCore yapılandırma seçenekleri
        }).AddRoles<Role>().AddEntityFrameworkStores<DemBackDbContext>();
    }
}

