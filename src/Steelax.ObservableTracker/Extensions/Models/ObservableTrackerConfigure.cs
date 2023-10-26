using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Steelax.ObservableTracker.Abstractions;
using Steelax.ObservableTracker.Core.Abstractions;
using Steelax.ObservableTracker.Core.Services;
using Steelax.ObservableTracker.Options;

namespace Steelax.ObservableTracker.Extensions.Models;

internal class ObservableTrackerConfigure : IObservableTrackerConfigure
{
    private readonly IServiceCollection _services;

    public ObservableTrackerConfigure(IServiceCollection services)
    {
        _services = services;
    }

    public IObservableTrackerConfigure UseHub<TTrackerHub, TImplementationTrackerHub>()
        where TTrackerHub : class
        where TImplementationTrackerHub : class, ITrackerHub, TTrackerHub
    {
        _services.TryAddSingleton<TTrackerHub, TImplementationTrackerHub>();
        _services.TryAddSingleton<IObservableTracker<TTrackerHub>, ObservableTrackerService<TTrackerHub>>();

        return this;
    }

    public IObservableTrackerConfigure ConfigureHubs(Func<HubQueueOptions, HubQueueOptions> configure)
    {
        var options = configure(new());

        _services.ConfigureOptions(options);

        return this;
    }

    public IObservableTrackerConfigure ConfigureChannels(Func<TrackerChannelOptions, TrackerChannelOptions> configure)
    {
        var options = configure(new());

        _services.ConfigureOptions(options);

        return this;
    }
}
