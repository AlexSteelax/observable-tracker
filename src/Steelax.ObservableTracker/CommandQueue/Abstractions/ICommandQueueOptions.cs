using System.Threading.Channels;

namespace Steelax.ObservableTracker.CommandQueue.Abstractions;

public interface ICommandQueueOptions
{
    /// <summary>
    /// Queue capacity
    /// </summary>
    int Capacity { get; }

    /// <summary>
    /// Queue command adding mode
    /// </summary>
    BoundedChannelFullMode FullMode { get; }

    /// <summary>
    /// Queue reading mode
    /// </summary>
    bool SingleReader { get; }

    /// <summary>
    /// Queue writing mode
    /// </summary>
    bool SingleWriter { get; }
}