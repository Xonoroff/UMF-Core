using System;
using System.Collections.Generic;

namespace UMF.Core.Infrastructure
{
    public interface ICommandExecutor : IEnumerator<ICommand>
    {
        int CurrentCommandIndex { get; }

        int TotalCommands { get; }

        Action<ICommand> OnCommandStartedExecution { get; set; }

        Action<ICommand> OnCommandCompleted { get; set; }

        Action<ICommand, Exception> OnCommandFailed { get; set; }
        Action<ICommand, float> OnCommandProgressChanged { get; set; }

        Action<bool> OnAllCompleted { get; set; }
        void Initialize(IEnumerable<ICommand> commandsToExecute);

        void StartExecution();
    }
}