using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Steelax.ObservableTracker.Abstractions;
using Steelax.ObservableTracker.CommandQueue.Services;
using Steelax.ObservableTracker.Core.Abstractions;
using Steelax.ObservableTracker.Core.Services;
using Steelax.ObservableTracker.Extensions.Models;
using Steelax.ObservableTracker.Options;

namespace Steelax.ObservableTracker.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddObservableTracker(this IServiceCollection services, Action<IObservableTrackerConfigure> configure)
    {
        services.TryAddSingleton(typeof(ICommandQueue<>), typeof(CommandQueueService<>));
        services.TryAddSingleton(typeof(ISubscriberCollection<>), typeof(SubscriberCollectionService<>));
        services.TryAddSingleton<ISubscriberLocator, SubscriberLocatorService>();
        services.TryAddTransient(typeof(TrackerChannelService<>));
        services.AddHostedService<CommandWorkerService<HubQueueOptions>>();
        services.AddHostedService<CommandWorkerService<TrackerChannelOptions>>();

        var cfg = new ObservableTrackerConfigure(services);

        configure(cfg);

        return services;
    }

    internal static IServiceCollection ConfigureOptions<T>(this IServiceCollection services, T instance)
        where T : class
    {
        services.AddSingleton(_ => Microsoft.Extensions.Options.Options.Create(instance));

        return services;
    }
}
