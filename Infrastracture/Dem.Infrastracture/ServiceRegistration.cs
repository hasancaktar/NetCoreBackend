using Dem.Application.Abstraction;
using Dem.Application.Abstraction.Token;
using Dem.Infrastracture.Jobs;
using Dem.Infrastracture.Services;
using Dem.Infrastracture.Token;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Dem.Infrastracture;

public static class ServiceRegistration
{
    public static void AddInfrastractureServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHandler, TokenHandler>();
        services.AddScoped<IMailService, MailService>();

        services.AddQuartz(options => { options.UseMicrosoftDependencyInjectionJobFactory(); });
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        services.ConfigureOptions<LoggingBackgoundJobSetup>();
    }
}