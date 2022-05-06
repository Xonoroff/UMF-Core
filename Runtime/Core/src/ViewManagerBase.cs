using System;
using System.Collections.Generic;
using System.Threading;
using Core.src;

namespace MF.Core.Scripts.Core.src
{
    public abstract class ViewManagerBase
    {
        protected List<Action> deInitializationCallbacks = new List<Action>();

        private CancellationToken cancellationToken;

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
            foreach (var callback in deInitializationCallbacks)
            {
                callback();
            }

            deInitializationCallbacks.Clear();
        }

        protected void AddDeInitialization(Action act)
        {
            deInitializationCallbacks.Add(act);
        }
    }
}