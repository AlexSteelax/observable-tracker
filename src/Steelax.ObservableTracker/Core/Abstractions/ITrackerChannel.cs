namespace Steelax.ObservableTracker.Core.Abstractions;

public interface ITrackerChannel<TMessage>
{
    ValueTask WriteAsync(TMessage message, CancellationToken cancellationToken = default);
    IAsyncEnumerable<TMessage> ReadAllAsync(CancellationToken cancellationToken = default);
    ValueTask UnsubscribeAsync(CancellationToken cancellationToken = default);

    Guid Id { get; }
}