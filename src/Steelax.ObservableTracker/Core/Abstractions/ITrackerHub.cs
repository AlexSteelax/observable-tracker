namespace Steelax.ObservableTracker.Core.Abstractions;

public interface ITrackerHub
{
    ValueTask UnsubscribeAsync<TMessage>(Guid trackerChannelId);
    ValueTask SubscribeAsync<TMessage>(Guid trackerChannelId);
}