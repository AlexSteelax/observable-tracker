using System.Diagnostics.CodeAnalysis;

namespace Steelax.ObservableTracker.Core.Abstractions;

internal interface ISubscriberCollection<TMessage>
{
    bool TryAdd(ITrackerChannel<TMessage> trackerChannel);
    void Remove(Guid trackerChannelId);
    bool TryGet(Guid trackerChannelId, [MaybeNullWhen(false)] out ITrackerChannel<TMessage> trackerChannel);
}