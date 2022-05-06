using System;
using Scripts.Core.src.Infrastructure;
using Zenject;

namespace Core.src.Utils
{
    public class ActivatorObjectFactory<T> : IFactorySync<T>
    {
        public T Create()
        {
            return Activator.CreateInstance<T>();
        }
    }
}