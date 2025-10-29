using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace UMF.Core.Infrastructure
{
    public class BaseCommandExecutor : ICommandExecutor
    {
        private readonly DiContainer container;
        private List<object> commandsToExecute = new List<object>();
        private List<object> failedCommands = new List<object>();
        private CancellationTokenSource currentCts;

        public BaseCommandExecutor(DiContainer container)
        {
            this.container = container;
        }

        public object this[int i] => commandsToExecute[i];

        object IEnumerator.Current => Current;

        public Action<object> OnCommandCompleted { get; set; }

        public Action<object, Exception> OnCommandFailed { get; set; }

        public Action<object, float> OnCommandProgressChanged { get; set; }

        public Action<bool> OnAllCompleted { get; set; }

        public void Initialize(IEnumerable<object> commandsToExecute)
        {
            if (commandsToExecute == null)
            {
                throw new ArgumentNullException(nameof(commandsToExecute));
            }

            this.commandsToExecute = new List<object>();
            foreach (var cmd in commandsToExecute)
            {
                if (cmd == null)
                {
                    this.commandsToExecute.Add(null);
                    continue;
                }

                // Accept only new-style ICommand<TResult>
                var iFaces = cmd.GetType().GetInterfaces();
                var isNewCommand = iFaces.Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICommand<>));
                if (isNewCommand)
                {
                    this.commandsToExecute.Add(cmd);
                }
                else
                {
                    Debug.LogWarning($"[UMF] Unsupported command type: {cmd.GetType().FullName}. Skipping.");
                }
            }

            failedCommands = new List<object>(commandsToExecute);
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

                OnCommandStartedExecution?.Invoke(Current);
                ExecuteCurrentAsync();
            }
            else
            {
                OnAllCompleted?.Invoke(failedCommands.Count == 0);
            }
        }

        public int CurrentCommandIndex { get; private set; }

        public int TotalCommands => commandsToExecute.Count;

        public Action<object> OnCommandStartedExecution { get; set; }

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

        public object Current =>
            CurrentCommandIndex < commandsToExecute.Count ? commandsToExecute[CurrentCommandIndex] : null;

        public void Dispose()
        {
            currentCts?.Dispose();
            currentCts = null;
        }

        private async void ExecuteCurrentAsync()
        {
            var cmd = Current;
            try
            {
                currentCts = new CancellationTokenSource();
                var cmdType = cmd.GetType();
                var iCmdOfT = cmdType.GetInterfaces().First(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICommand<>));
                var resultType = iCmdOfT.GetGenericArguments()[0];
                var handlerType = typeof(ICommandHandler<,>).MakeGenericType(cmdType, resultType);
                var handler = container.Resolve(handlerType);

                var execMethod = handlerType.GetMethod("ExecuteAsync");
                IProgress<float> progress = OnCommandProgressChanged != null
                    ? new Progress<float>(p =>
                    {
                        try
                        {
                            OnCommandProgressChanged?.Invoke(cmd, p);
                        }
                        catch (Exception e)
                        {
                            Debug.LogException(e);
                        }
                    })
                    : null;

                var task = (Task)execMethod.Invoke(handler, new object[] { cmd, currentCts.Token, progress });
                await task.ConfigureAwait(false);

                // Notify completion and advance
                OnCommandCompleted?.Invoke(cmd);
                StartExecution();
            }
            catch (OperationCanceledException oce)
            {
                failedCommands.Add(cmd);
                OnCommandFailed?.Invoke(cmd, oce);
                StartExecution();
            }
            catch (Exception ex)
            {
                var e = ex is TargetInvocationException tie && tie.InnerException != null ? tie.InnerException : ex;
                failedCommands.Add(cmd);
                OnCommandFailed?.Invoke(cmd, e);
                StartExecution();
            }
        }
    }
}