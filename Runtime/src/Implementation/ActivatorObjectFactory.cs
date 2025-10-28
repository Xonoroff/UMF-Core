using System;

namespace UMF.Core.Implementation
{
    public class ActivatorObjectFactory<T> : IFactorySync<T>
    {
        public T Create()
        {
            return Activator.CreateInstance<T>();
        }
    }
}