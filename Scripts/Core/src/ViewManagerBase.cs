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
        
        protected virtual void Initialize()
        {
            cancellationToken = new CancellationToken();
        }

        protected virtual void DeInitialize()
        {
            foreach (var callback in deInitializationCallbacks)
            {
                callback();
            }

            GetAssetLoader()?.ReleaseAllLoadedAssets();
            deInitializationCallbacks.Clear();
        }

        protected abstract IAssetLoader GetAssetLoader();
    }
}