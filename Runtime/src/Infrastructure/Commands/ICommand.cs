using System;

namespace UMF.Core.Infrastructure
{
    public interface ICommand
    {
        string Description { get; set; }

        int Priority { get; }

        Action OnSuccess { get; set; }

        Action<float> OnProgressChanged { get; set; }

        Action<Exception> OnFail { get; set; }
        void Execute();

        void Undo();

        bool IsAvailable();
    }
}