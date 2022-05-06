using System;
using Core.src.Infrastructure;

namespace Core.src.Entity
{
    public abstract class BaseCommand : ICommand
    {
        public abstract void Execute();

        public abstract void Undo();

        public abstract bool IsAvailable();

        public abstract string Description { get; set; }
        
        public abstract int Priority { get; }
        
        public Action OnSuccess { get; set; }
        
        public Action<float> OnProgressChanged { get; set; }

        public Action<Exception> OnFail { get; set; }
    }
}