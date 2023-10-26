using Steelax.ObservableTracker.Core.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace Steelax.ObservableTracker.Core.Services;

internal class SubscriberCollectionService<TMessage> : ISubscriberCollection<TMessage>
{
    private readonly Dictionary<Guid, ITrackerChannel<TMessage>> _subscribers = new();

    public bool TryAdd(ITrackerChannel<TMessage> trackerChannel) => _subscribers.TryAdd(trackerChannel.Id, trackerChannel);

    public void Remove(Guid trackerChannelId)
    {
        _subscribers.Remove(trackerChannelId);
    }

    public bool TryGet(Guid trackerChannelId, [MaybeNullWhen(false)] out ITrackerChannel<TMessage> trackerChannel) => _subscribers.TryGetValue(trackerChannelId, out trackerChannel);
}