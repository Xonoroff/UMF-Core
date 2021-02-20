using System;

namespace Core.src.Infrastructure
{
    public interface ICommand
    {
        void Execute();

        void Undo();

        bool IsAvailable();

        string Description { get; set; }
    
        int Priority { get; }

        Action OnSuccess { get; set; }

        Action<float> OnProgressChanged { get; set; }

        Action<Exception> OnFail { get; set; }
    }
}
