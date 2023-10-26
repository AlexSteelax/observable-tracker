namespace Steelax.ObservableTracker.CommandQueue.Abstractions;

internal interface ICommand
{
    /// <summary>
    /// Run command
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task ExecuteAsync(CancellationToken cancellationToken);
}