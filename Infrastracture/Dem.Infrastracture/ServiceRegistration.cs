using Dem.Application.Abstraction.Token;
using Dem.Infrastracture.Token;
using Microsoft.Extensions.DependencyInjection;


namespace Dem.Infrastracture;

public static class ServiceRegistration
{
    public static void AddInfrastractureServices(this IServiceCollection services)
    {
        services.AddTransient<ITokenHandler, TokenHandler>();         
    }
}

