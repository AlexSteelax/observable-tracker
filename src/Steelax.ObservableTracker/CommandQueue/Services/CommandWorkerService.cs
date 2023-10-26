using Microsoft.Extensions.Hosting;
using Steelax.ObservableTracker.Abstractions;
using Steelax.ObservableTracker.CommandQueue.Abstractions;

namespace Steelax.ObservableTracker.CommandQueue.Services;

internal class CommandWorkerService<TOptions> : BackgroundService
    where TOptions : class, ICommandQueueOptions
{
    private readonly ICommandQueue<TOptions> _queue;

    public CommandWorkerService(ICommandQueue<TOptions> queue)
    {
        _queue = queue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var cmd in _queue.ReadAllAsync(stoppingToken))
        {
            await cmd.ExecuteAsync(stoppingToken);
        }
    }
}
