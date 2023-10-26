namespace Steelax.ObservableTracker.CommandQueue.Abstractions;

internal interface ICommandWaiter<TResult>
{
    Task<TResult> WaitAsync();
}

internal interface ICommandWaiter
{
    Task WaitAsync();
}