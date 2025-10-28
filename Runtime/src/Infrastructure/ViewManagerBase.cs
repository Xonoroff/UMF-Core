using System;
using System.Collections.Generic;

namespace UMF.Core.Infrastructure
{
    public abstract class ViewManagerBase
    {
        private CancellationToken cancellationToken;
        protected List<Action> deInitializationCallbacks = new();

        public ViewManagerBase()
        {
            cancellationToken = new CancellationToken();
            cancellationToken.Register(DeInitialize);
        }

        protected void Initialize(CancellationToken ct)
        {
        }

        protected virtual void DeInitialize()
        {
            foreach (var callback in deInitializationCallbacks) callback();

            deInitializationCallbacks.Clear();
        }

        protected void AddDeInitialization(Action act)
        {
            deInitializationCallbacks.Add(act);
        }
    }
}