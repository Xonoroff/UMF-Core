using System;
using System.Threading;
using System.Threading.Tasks;

namespace UMF.Core.Infrastructure
{
    /// <summary>
    /// Handles execution of a command instance asynchronously, optionally reporting progress and supporting cancellation.
    /// </summary>
    /// <typeparam name="TCommand">The command type.</typeparam>
    /// <typeparam name="TResult">The result type returned by the command.</typeparam>
    public interface ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        Task<TResult> ExecuteAsync(
            TCommand command,
            CancellationToken cancellationToken = default,
            IProgress<float> progress = null);
    }
}
