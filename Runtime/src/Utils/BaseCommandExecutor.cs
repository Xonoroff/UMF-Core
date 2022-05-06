using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.src.Infrastructure;
using UnityEngine;

namespace Core.src.Utils
{
    public class BaseCommandExecutor : ICommandExecutor
    {
        private List<ICommand> commandsToExecute;

        private List<ICommand> failedCommands;

        private int currentExecutionIndex;
        
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

        public ICommand this[int i] => commandsToExecute[i];
        public void StartExecution()
        {
            if (MoveNext())
            {
                if (Current == null)
                {
                    Debug.LogError($"Found null command!");

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

        public int CurrentCommandIndex => currentExecutionIndex;

        public int TotalCommands => commandsToExecute.Count;

        public Action<ICommand> OnCommandStartedExecution { get; set; }

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

        public bool MoveNext()
        {
            if (currentExecutionIndex < commandsToExecute.Count - 1)
            {
                currentExecutionIndex++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            currentExecutionIndex = -1;
        }

        object IEnumerator.Current => Current;

        public ICommand Current => currentExecutionIndex < commandsToExecute.Count ? commandsToExecute[currentExecutionIndex] : null;

        public void Dispose()
        {
            
        }
    }
}