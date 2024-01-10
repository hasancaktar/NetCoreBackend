using Dem.Application.Abstraction;
using Dem.Application.Abstraction.Token;
using Dem.Infrastracture.Services;
using Dem.Infrastracture.Token;
using Microsoft.Extensions.DependencyInjection;


namespace Dem.Infrastracture;

public static class ServiceRegistration
{
    public static void AddInfrastractureServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHandler, TokenHandler>();
        services.AddScoped<IMailService, MailService>();

    }
}

