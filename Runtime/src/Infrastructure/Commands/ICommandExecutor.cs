using System;
using System.Collections.Generic;

namespace UMF.Core.Infrastructure
{
    /// <summary>
    /// Executes new-style commands (objects implementing ICommand&lt;TResult&gt;) sequentially via their ICommandHandler.
    /// Legacy ICommand is no longer supported.
    /// </summary>
    public interface ICommandExecutor : IEnumerator<object>
    {
        int CurrentCommandIndex { get; }

        int TotalCommands { get; }

        Action<object> OnCommandStartedExecution { get; set; }

        Action<object> OnCommandCompleted { get; set; }

        Action<object, Exception> OnCommandFailed { get; set; }
        Action<object, float> OnCommandProgressChanged { get; set; }

        Action<bool> OnAllCompleted { get; set; }
        void Initialize(IEnumerable<object> commandsToExecute);

        void StartExecution();
    }
}