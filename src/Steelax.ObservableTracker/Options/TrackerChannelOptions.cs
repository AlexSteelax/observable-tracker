using Steelax.ObservableTracker.CommandQueue.Models;

namespace Steelax.ObservableTracker.Options;

public class TrackerChannelOptions : CommandQueueOptions<TrackerChannelOptions>
{
    public TrackerChannelOptions()
    {
        SingleReader = true;
    }

    public new TrackerChannelOptions SetCapacity(int value) => base.SetCapacity(value);
    public new TrackerChannelOptions SetSingleWriter(bool value) => base.SetSingleWriter(value);
}