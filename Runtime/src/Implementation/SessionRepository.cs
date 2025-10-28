using System;
using UMF.Core.Infrastructure;

namespace UMF.Core.Implementation
{
    public class SessionRepository<T> : ISessionRepository<T>
    {
        private T cachedData;

        public T Get()
        {
            if (EqualityComparer<T>.Default.Equals(cachedData, default(T)))
            {
                var newInstance = Activator.CreateInstance<T>();
                Update(newInstance);
            }

            return cachedData;
        }

        public void Update(T data)
        {
            cachedData = data;
        }
    }
}