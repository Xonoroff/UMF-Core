using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UMF.Core.Infrastructure
{
    public abstract class AssetLoaderBase : IAssetLoader
    {
        private readonly IAssetProvider assetProvider;
        protected List<Object> loadedAssets = new();

        public AssetLoaderBase(IAssetProvider assetsProvide)
        {
            assetProvider = assetsProvide;
        }

        public async UniTask<T> LoadAssetAsync<T>(string assetKey, CancellationToken cancellationToken) where T : Object
        {
            var loadedResource = await assetProvider.ProvideAsset<T>(assetKey, cancellationToken);
            if (cancellationToken.IsCancellationRequested)
            {
                if (loadedResource != null) assetProvider.ReleaseAsset(loadedResource);

                return null;
            }

            loadedAssets.Add(loadedResource);
            return loadedResource;
        }

        public void ReleaseAllLoadedAssets()
        {
            foreach (var loadedAsset in loadedAssets) assetProvider.ReleaseAsset(loadedAsset);
        }
    }
}