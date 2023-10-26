using Steelax.ObservableTracker.Core.Abstractions;
using Zyfra.TPlus.StreamTracking.Abstractions;

namespace Steelax.ObservableTracker.Abstractions;

/// <summary>
/// Main tracker service interface
/// <para>[thread-safe]</para>
/// </summary>
/// <typeparam name="TTrackerHub"></typeparam>
public interface IObservableTracker<TTrackerHub>
    where TTrackerHub : class
{
    /// <summary>
    /// Subscribe a client and get his channel
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<ITrackerChannel<TMessage>> SubscribeAsync<TMessage>(TrackerChannelConfigure? configure = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Write hub command with execution result waiting
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="factory"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<TResult> WriteAndWaitAsync<TResult>(Func<TTrackerHub, TResult> factory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Write hub command with execution result waiting
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="factory"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<TResult> WriteAndWaitAsync<TResult>(Func<TTrackerHub, Task<TResult>> factory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Write hub command with execution result waiting
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="factory"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<TResult> WriteAndWaitAsync<TResult>(Func<TTrackerHub, ValueTask<TResult>> factory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Write hub command with execution result waiting
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask WriteAndWaitAsync(Action<TTrackerHub> factory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Write hub command with execution result waiting
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask WriteAndWaitAsync(Func<TTrackerHub, Task> factory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Write hub command with execution result waiting
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask WriteAndWaitAsync(Func<TTrackerHub, ValueTask> factory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Write hub command without execution waiting
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask WriteAsync(Action<TTrackerHub> factory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Write hub command without execution waiting
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask WriteAsync(Func<TTrackerHub, Task> factory, CancellationToken cancellationToken = default);

    /// <summary>
    /// Write hub command without execution waiting
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask WriteAsync(Func<TTrackerHub, ValueTask> factory, CancellationToken cancellationToken = default);
}