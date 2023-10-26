using Steelax.ObservableTracker.CommandQueue.Abstractions;
using System.Threading.Channels;

namespace Steelax.ObservableTracker.CommandQueue.Models;

public abstract class CommandQueueOptions<TSelft> : ICommandQueueOptions
    where TSelft : CommandQueueOptions<TSelft>, new()
{
    public int Capacity { get; internal set; } = 10;

    public BoundedChannelFullMode FullMode { get; internal set; } = BoundedChannelFullMode.Wait;

    public bool SingleReader { get; internal set; } = false;

    public bool SingleWriter { get; internal set; } = false;

    private TSelft Create() => new() { Capacity = Capacity, FullMode = FullMode, SingleReader = SingleReader, SingleWriter = SingleWriter };

    internal TSelft SetCapacity(int value)
    {
        var instance = Create();
        instance.Capacity = value;
        return instance;
    }

    internal TSelft SetFullMode(BoundedChannelFullMode value)
    {
        var instance = Create();
        instance.FullMode = value;
        return instance;
    }

    internal TSelft SetSingleReader(bool value)
    {
        var instance = Create();
        instance.SingleReader = value;
        return instance;
    }

    internal TSelft SetSingleWriter(bool value)
    {
        var instance = Create();
        instance.SingleWriter = value;
        return instance;
    }
}