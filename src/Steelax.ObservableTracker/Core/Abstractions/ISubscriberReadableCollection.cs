using System.Diagnostics.CodeAnalysis;

namespace Steelax.ObservableTracker.Core.Abstractions;

public interface ISubscriberReadableCollection<TMessage>
{
    bool TryGet(Guid trackerChannelId, [MaybeNullWhen(false)] out ITrackerChannel<TMessage> trackerChannel);
}