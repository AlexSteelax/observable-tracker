using Microsoft.Extensions.Options;
using Steelax.ObservableTracker.Abstractions;
using Steelax.ObservableTracker.CommandQueue.Abstractions;
using System.Threading.Channels;

namespace Steelax.ObservableTracker.CommandQueue.Services;

internal class CommandQueueService<TOptions> : ICommandQueue<TOptions>
    where TOptions : class, ICommandQueueOptions
{
    private readonly Channel<ICommand> _pipeline;
    public CommandQueueService(IOptions<TOptions> options)
    {
        var channelOptions = new BoundedChannelOptions(options.Value.Capacity)
        {
            FullMode = options.Value.FullMode,
            SingleReader = options.Value.SingleReader,
            SingleWriter = options.Value.SingleWriter
        };

        _pipeline = Channel.CreateBounded<ICommand>(channelOptions);
    }

    public ValueTask WriteAsync(ICommand command, CancellationToken cancellationToken) => _pipeline.Writer.WriteAsync(command, cancellationToken);

    public IAsyncEnumerable<ICommand> ReadAllAsync(CancellationToken cancellationToken) => _pipeline.Reader.ReadAllAsync(cancellationToken);
}