using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steelax.ObservableTracker.Abstractions;
using Steelax.ObservableTracker.CommandQueue.Abstractions;

namespace Steelax.ObservableTracker.CommandQueue.Services;

internal class CommandWorkerService<TOptions> : BackgroundService
    where TOptions : class, ICommandQueueOptions
{
    private readonly ICommandQueue<TOptions> _queue;
    private readonly ILogger<CommandWorkerService<TOptions>> _logger;

    public CommandWorkerService(ICommandQueue<TOptions> queue, ILogger<CommandWorkerService<TOptions>> logger)
    {
        _queue = queue;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var cmd in _queue.ReadAllAsync(stoppingToken))
        {
            try
            {
                await cmd.ExecuteAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Command executing has error. The object status in the database may be incorrect.");
            }
        }
    }
}