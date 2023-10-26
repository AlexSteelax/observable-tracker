using Microsoft.Extensions.DependencyInjection;
using Steelax.ObservableTracker.Core.Abstractions;

namespace Steelax.ObservableTracker.Core.Services;

internal class SubscriberLocatorService : ISubscriberLocator
{
    private readonly IServiceProvider _provider;

    public SubscriberLocatorService(IServiceProvider provider)
    {
        _provider = provider;
    }

    public ISubscriberReadableCollection<TMessage> GetSubscriberCollection<TMessage>()
    {
        var subscribers = _provider.GetRequiredService<ISubscriberCollection<TMessage>>();

        return subscribers;
    }
}
