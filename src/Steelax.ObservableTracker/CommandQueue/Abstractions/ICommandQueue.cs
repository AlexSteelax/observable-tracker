using Steelax.ObservableTracker.CommandQueue.Abstractions;

namespace Steelax.ObservableTracker.Abstractions;

internal interface ICommandQueue<TOptions>
    where TOptions : class, ICommandQueueOptions
{
    ValueTask WriteAsync(ICommand command, CancellationToken cancellationToken);

    IAsyncEnumerable<ICommand> ReadAllAsync(CancellationToken cancellationToken);
}