namespace Steelax.ObservableTracker.Core.Abstractions;

internal interface ISubscriberCollection<TMessage> : ISubscriberReadableCollection<TMessage>
{
    bool TryAdd(ITrackerChannel<TMessage> trackerChannel);
    void Remove(Guid trackerChannelId);
}