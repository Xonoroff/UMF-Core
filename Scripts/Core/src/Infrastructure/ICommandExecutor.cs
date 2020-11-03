using System;
using System.Collections.Generic;

namespace Core.src.Infrastructure
{
    public interface ICommandExecutor : IEnumerator<ICommand>
    {
        void Initialize(IEnumerable<ICommand> commandsToExecute);
        
        void StartExecution();
        
        int CurrentCommandIndex { get; }
        
        int TotalCommands { get; }

        Action<ICommand> OnCommandStartedExecution { get; set; }

        Action<ICommand> OnCommandCompleted { get; set; }

        Action<ICommand, Exception> OnCommandFailed { get; set; }

        Action<bool> OnAllCompleted { get; set; }
    }
}