using Steelax.ObservableTracker.Abstractions;
using Steelax.ObservableTracker.Core.Abstractions;
using Steelax.ObservableTracker.Core.Models;
using Steelax.ObservableTracker.Core.Models.Commands;
using Steelax.ObservableTracker.Options;
using System.Threading.Channels;

namespace Steelax.ObservableTracker.Core.Services;

/// <summary>
/// Объект подписчика
/// [thread-safe]
/// </summary>
/// <typeparam name="TMessage"></typeparam>
internal class TrackerChannelService<TMessage> : ITrackerChannel<TMessage>
{
    private readonly ICommandQueue<TrackerChannelOptions> _queue;
    private readonly ISubscriberCollection<TMessage> _subscribers;

    private Channel<TMessage>? _channel;
    private ITrackerHub? _hub;

    private readonly Guid _id;
    private bool _completed;

    public TrackerChannelService(ICommandQueue<TrackerChannelOptions> queue, ISubscriberCollection<TMessage> subscribers)
    {
        _id = Guid.NewGuid();
        _queue = queue;
        _subscribers = subscribers;
    }

    public Guid Id => _id;

    public async ValueTask<TrackerChannelService<TMessage>> RegisterAsync(ITrackerHub hub, TrackerChannelOptions options)
    {
        var channelOptions = new BoundedChannelOptions(options.Capacity)
        {
            FullMode = options.FullMode,
            SingleReader = options.SingleReader,
            SingleWriter = options.SingleWriter
        };

        _hub = hub;
        _channel = Channel.CreateBounded<TMessage>(channelOptions);

        _subscribers.TryAdd(this);
        await _hub!.SubscribeAsync<TMessage>(_id);

        return this;
    }

    public ValueTask WriteAsync(TMessage message, CancellationToken cancellationToken = default)
    {
        return _channel!.Writer.WriteAsync(message, cancellationToken);
    }

    public IAsyncEnumerable<TMessage> ReadAllAsync(CancellationToken cancellationToken = default)
    {
        return _channel!.Reader.ReadAllAsync(cancellationToken);
    }

    public ValueTask UnsubscribeAsync(CancellationToken cancellationToken = default)
    {
        var cmd = new Command(Unsubscribe);
        return _queue.WriteAsync(cmd, cancellationToken);
    }

    /// <summary>
    /// [non-thread-safe]
    /// </summary>
    internal async ValueTask Unsubscribe()
    {
        if (_completed)
            return;

        _subscribers.Remove(_id);
        await _hub!.UnsubscribeAsync<TMessage>(_id);
        _channel!.Writer.TryComplete();

        _completed = true;
    }
}