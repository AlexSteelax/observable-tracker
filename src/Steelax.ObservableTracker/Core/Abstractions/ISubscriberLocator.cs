using System.Diagnostics.CodeAnalysis;

namespace Steelax.ObservableTracker.Core.Abstractions;

public interface ISubscriberLocator
{
    bool TryGetTrackerChannel<TMessage>(Guid trackerChannelId, [MaybeNullWhen(false)] out ITrackerChannel<TMessage> trackerChannel);
}
