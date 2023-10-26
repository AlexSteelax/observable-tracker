using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Steelax.ObservableTracker.Abstractions;
using Steelax.ObservableTracker.Core.Abstractions;
using Steelax.ObservableTracker.Core.Models.Commands;
using Steelax.ObservableTracker.Options;
using Zyfra.TPlus.StreamTracking.Abstractions;

namespace Steelax.ObservableTracker.Core.Services;

internal class ObservableTrackerService<TTrackerHub> : IObservableTracker<TTrackerHub>
    where TTrackerHub : class
{
    private readonly ICommandQueue<TrackerChannelOptions> _queue;
    private readonly ICommandQueue<HubQueueOptions> _queueHub;
    private readonly TTrackerHub _hub;
    private readonly ITrackerHub _hubTracker;
    private readonly IServiceProvider _provider;
    private readonly IOptions<TrackerChannelOptions> _channelOptions;

    public ObservableTrackerService(
        ICommandQueue<TrackerChannelOptions> queue,
        ICommandQueue<HubQueueOptions> queueHub,
        TTrackerHub hub,
        IOptions<TrackerChannelOptions> channelOptions,
        IServiceProvider provider)
    {
        _queue = queue;
        _queueHub = queueHub;
        _hub = hub;
        _hubTracker = hub as ITrackerHub ?? throw new InvalidCastException();
        _provider = provider;
        _channelOptions = channelOptions;
    }

    public async ValueTask<TResult> WriteAndWaitAsync<TResult>(Func<TTrackerHub, TResult> factory, CancellationToken cancellationToken = default)
    {
        var cmd = new Command<TResult>(() => factory(_hub));
        await _queueHub.WriteAsync(cmd, cancellationToken);
        var ret = await cmd.WaitAsync();
        return ret;
    }

    public async ValueTask<TResult> WriteAndWaitAsync<TResult>(Func<TTrackerHub, Task<TResult>> factory, CancellationToken cancellationToken = default)
    {
        var cmd = new Command<TResult>(() => factory(_hub));
        await _queueHub.WriteAsync(cmd, cancellationToken);
        var ret = await cmd.WaitAsync();
        return ret;
    }

    public async ValueTask WriteAndWaitAsync(Action<TTrackerHub> factory, CancellationToken cancellationToken = default)
    {
        var cmd = new Command(() => factory(_hub));
        await _queueHub.WriteAsync(cmd, cancellationToken);
        await cmd.WaitAsync();
        return;
    }

    public async ValueTask WriteAndWaitAsync(Func<TTrackerHub, Task> factory, CancellationToken cancellationToken = default)
    {
        var cmd = new Command(() => factory(_hub));
        await _queueHub.WriteAsync(cmd, cancellationToken);
        await cmd.WaitAsync();
        return;
    }

    public ValueTask WriteAsync(Action<TTrackerHub> factory, CancellationToken cancellationToken = default)
    {
        var cmd = new Command(() => factory(_hub));
        return _queueHub.WriteAsync(cmd, cancellationToken);
    }

    public ValueTask WriteAsync(Func<TTrackerHub, Task> factory, CancellationToken cancellationToken = default)
    {
        var cmd = new Command(() => factory(_hub));
        return _queueHub.WriteAsync(cmd, cancellationToken);
    }

    public async ValueTask<ITrackerChannel<TMessage>> SubscribeAsync<TMessage>(TrackerChannelConfigure? configure = default, CancellationToken cancellationToken = default)
    {
        var options = configure is null ? _channelOptions.Value : configure(_channelOptions.Value);

        var cmd = new Command<ITrackerChannel<TMessage>>(() => Subscribe<TMessage>(options));
        await _queue.WriteAsync(cmd, cancellationToken);
        var ret = await cmd.WaitAsync();
        return ret;
    }

    /// <summary>
    /// [non-thread-safe]
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    /// <returns></returns>
    internal async ValueTask<ITrackerChannel<TMessage>> Subscribe<TMessage>(TrackerChannelOptions options)
    {
        return await _provider.GetRequiredService<TrackerChannelService<TMessage>>().RegisterAsync(_hubTracker, options);
    }
}
