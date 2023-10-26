using Steelax.ObservableTracker.Core.Abstractions;
using Steelax.ObservableTracker.Options;

namespace Steelax.ObservableTracker.Abstractions;

public interface IObservableTrackerConfigure
{
    IObservableTrackerConfigure UseHub<TTrackerHub, TImplementationTrackerHub>()
        where TTrackerHub : class
        where TImplementationTrackerHub : class, TTrackerHub, ITrackerHub;

    IObservableTrackerConfigure ConfigureHubs(Func<HubQueueOptions, HubQueueOptions> configure);
    IObservableTrackerConfigure ConfigureChannels(Func<TrackerChannelOptions, TrackerChannelOptions> configure);
}