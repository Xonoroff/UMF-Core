using System.Collections.Generic;
using System.Threading;

#if UNITASK_ENABLED
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

using UnityEngine;

namespace Core.src
{
        public abstract class AssetLoaderBase : IAssetLoader
    {
        protected List<Object> loadedAssets = new List<Object>();

        private IAssetProvider assetProvider;

        public AssetLoaderBase(IAssetProvider assetsProvide)
        {
            this.assetProvider = assetsProvide;
        }


 
    
#if UNITASK_ENABLED
        public async UniTask<T> LoadAssetAsync<T>(string assetKey, CancellationToken cancellationToken) where T : Object
#else 
        public async Task<T> LoadAssetAsync<T>(string assetKey, CancellationToken cancellationToken) where T : Object
#endif
        {
            var loadedResource = await assetProvider.ProvideAsset<T>(assetKey, cancellationToken);
            if (cancellationToken.IsCancellationRequested)
            {
                if (loadedResource != null)
                {
                    assetProvider.ReleaseAsset(loadedResource);
                }

                return null;
            }
            
            loadedAssets.Add(loadedResource);
            return loadedResource;
        }

        public void ReleaseAllLoadedAssets()
        {
            foreach (var loadedAsset in loadedAssets)
            {
                assetProvider.ReleaseAsset(loadedAsset);
            }
        }
    }
}