namespace UMF.Core.Infrastructure
{
    /// <summary>
    /// Marker for commands with a typed response.
    /// This represents the intent (what to do) as data.
    /// The behavior is implemented by an ICommandHandler.
    /// </summary>
    /// <typeparam name="TResult">The type of the result returned when the command completes.</typeparam>
    public interface ICommand<TResult>
    {
    }
}
