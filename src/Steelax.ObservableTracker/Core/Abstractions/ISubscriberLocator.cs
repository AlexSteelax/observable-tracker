namespace Steelax.ObservableTracker.Core.Abstractions;

public interface ISubscriberLocator
{
    ISubscriberReadableCollection<TMessage> GetSubscriberCollection<TMessage>();
}
