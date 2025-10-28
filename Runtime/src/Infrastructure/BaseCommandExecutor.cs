using System;
using System.Collections.Generic;

namespace UMF.Core.Infrastructure
{
    public class BaseCommandExecutor : ICommandExecutor
    {
        private List<ICommand> commandsToExecute;

        private List<ICommand> failedCommands;

        public ICommand this[int i] => commandsToExecute[i];

        object IEnumerator.Current => Current;

        public Action<ICommand> OnCommandCompleted { get; set; }

        public Action<ICommand, Exception> OnCommandFailed { get; set; }

        public Action<ICommand, float> OnCommandProgressChanged { get; set; }

        public Action<bool> OnAllCompleted { get; set; }

        public void Initialize(IEnumerable<ICommand> commandsToExecute)
        {
            this.commandsToExecute = commandsToExecute.OrderBy(x => x.Priority).ToList();
            failedCommands = new List<ICommand>();
            Reset();
        }

        public void StartExecution()
        {
            if (MoveNext())
            {
                if (Current == null)
                {
                    Debug.LogError("Found null command!");

                    failedCommands.Add(null);
                    StartExecution();
                    return;
                }

                if (!Current.IsAvailable())
                {
                    StartExecution();
                    return;
                }

                Current.OnSuccess = OnCommandCompletedHandler;
                Current.OnFail = OnCommandFailHandler;
                Current.OnProgressChanged = OnCommandProgressChangedHandler;
                OnCommandStartedExecution?.Invoke(Current);
                Current.Execute();
            }
            else
            {
                OnAllCompleted?.Invoke(failedCommands.Count == 0);
            }
        }

        public int CurrentCommandIndex { get; private set; }

        public int TotalCommands => commandsToExecute.Count;

        public Action<ICommand> OnCommandStartedExecution { get; set; }

        public bool MoveNext()
        {
            if (CurrentCommandIndex < commandsToExecute.Count - 1)
            {
                CurrentCommandIndex++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            CurrentCommandIndex = -1;
        }

        public ICommand Current =>
            CurrentCommandIndex < commandsToExecute.Count ? commandsToExecute[CurrentCommandIndex] : null;

        public void Dispose()
        {
        }

        private void OnCommandCompletedHandler()
        {
            UnsubscribeCommand(Current);
            OnCommandCompleted?.Invoke(Current);
            StartExecution();
        }

        private void OnCommandProgressChangedHandler(float progress)
        {
            OnCommandProgressChanged?.Invoke(Current, progress);
        }

        private void OnCommandFailHandler(Exception obj)
        {
            Debug.LogError(Current != null
                ? $"Command failed! {Current.Description} with message {obj}"
                : $"On command failed in executor! Found null command error = {obj}");

            UnsubscribeCommand(Current);
            failedCommands.Add(Current);
            OnCommandFailed?.Invoke(Current, obj);
            Current?.Undo();
            StartExecution();
        }

        private void UnsubscribeCommand(ICommand command)
        {
            if (command?.OnSuccess != null) Current.OnSuccess -= OnCommandCompletedHandler;
            if (command?.OnFail != null) Current.OnFail -= OnCommandFailHandler;
        }
    }
}