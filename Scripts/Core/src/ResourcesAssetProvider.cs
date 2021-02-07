using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.src
{
    public class ResourcesAssetProvider : IAssetProvider
    {
        public async UniTask<T> ProvideAsset<T>(string assetKey, CancellationToken cancellationToken) where T : Object
        {
            var loadedResource = await Resources.LoadAsync<GameObject>(assetKey).WithCancellation(cancellationToken);
            
            return loadedResource as T;
        }

        public void ReleaseAsset<T>(T loadedAsset) where T : Object
        {
            Resources.UnloadAsset(loadedAsset);
        }
    }
}