using Steelax.ObservableTracker.CommandQueue.Abstractions;

namespace Steelax.ObservableTracker.Core.Models.Commands;

internal class Command : ICommand, ICommandWaiter
{
    private readonly Func<TaskCompletionSource, ValueTask> _action;
    private readonly TaskCompletionSource _tsource;

    public Command(Func<Task> handle)
    {
        _action = async (tsource) =>
        {
            await handle();
            tsource.SetResult();
        };
        _tsource = new();
    }

    public Command(Func<ValueTask> handle)
    {
        _action = async (tsource) =>
        {
            await handle();
            tsource.SetResult();
        };
        _tsource = new();
    }

    public Command(Action handle)
    {
        _action = (tsource) =>
        {
            handle();
            tsource.SetResult();

            return ValueTask.CompletedTask;
        };
        _tsource = new();
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            _tsource.SetCanceled(cancellationToken);

        try
        {
            await _action(_tsource);
        }
        catch (Exception ex)
        {
            _tsource.SetException(ex);
        }
    }

    public Task WaitAsync() => _tsource.Task;
}