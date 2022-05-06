using System.Collections.Generic;
using System.Threading;

using Cysharp.Threading.Tasks;
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

        public async UniTask<T> LoadAssetAsync<T>(string assetKey, CancellationToken cancellationToken) where T : Object
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