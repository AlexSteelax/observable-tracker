using Steelax.ObservableTracker.CommandQueue.Models;

namespace Steelax.ObservableTracker.Options;


public class HubQueueOptions : CommandQueueOptions<HubQueueOptions>
{
    public HubQueueOptions()
    {
        SingleReader = true;
    }

    public new HubQueueOptions SetCapacity(int value) => base.SetCapacity(value);
    public new HubQueueOptions SetSingleWriter(bool value) => base.SetSingleWriter(value);
}