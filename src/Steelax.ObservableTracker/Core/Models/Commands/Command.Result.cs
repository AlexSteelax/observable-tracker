using Steelax.ObservableTracker.CommandQueue.Abstractions;

namespace Steelax.ObservableTracker.Core.Models.Commands;

internal class Command<TResult> : ICommand, ICommandWaiter<TResult>
{
    private readonly Func<TaskCompletionSource<TResult>, ValueTask> _action;
    private readonly TaskCompletionSource<TResult> _tsource;

    public Command(Func<Task<TResult>> handle)
    {
        _action = async (tsource) =>
        {
            var ret = await handle();
            tsource.SetResult(ret);
        };
        _tsource = new();
    }

    public Command(Func<ValueTask<TResult>> handle)
    {
        _action = async (tsource) =>
        {
            var ret = await handle();
            tsource.SetResult(ret);
        };
        _tsource = new();
    }

    public Command(Func<TResult> handle)
    {
        _action = (tsource) =>
        {
            var ret = handle();
            tsource.SetResult(ret);

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

    public Task<TResult> WaitAsync() => _tsource.Task;
}
