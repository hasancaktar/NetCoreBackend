using Microsoft.Extensions.Logging;
using Quartz;

namespace Dem.Infrastracture.Jobs;

[DisallowConcurrentExecution]
public class LoggingBackgoundJob : IJob
{
    private readonly ILogger<LoggingBackgoundJob> _logger;

    public LoggingBackgoundJob(ILogger<LoggingBackgoundJob> logger)
    {
        _logger = logger;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("{UtcNow}", DateTime.UtcNow);
        return Task.CompletedTask;
    }
}