using Microsoft.Extensions.DependencyInjection;
using Steelax.ObservableTracker.Core.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace Steelax.ObservableTracker.Core.Services;

internal class SubscriberLocatorService : ISubscriberLocator
{
    private readonly IServiceProvider _provider;

    public SubscriberLocatorService(IServiceProvider provider)
    {
        _provider = provider;
    }

    public bool TryGetTrackerChannel<TMessage>(Guid trackerChannelId, [MaybeNullWhen(false)] out ITrackerChannel<TMessage> trackerChannel)
    {
        var subscribers = _provider.GetRequiredService<ISubscriberCollection<TMessage>>();


        return subscribers.TryGet(trackerChannelId, out trackerChannel);
    }
}
