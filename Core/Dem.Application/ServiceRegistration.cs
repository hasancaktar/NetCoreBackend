using Microsoft.Extensions.DependencyInjection;

namespace Dem.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));
    }
}

