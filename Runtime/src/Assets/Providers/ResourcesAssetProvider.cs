using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

using Cysharp.Threading.Tasks;

namespace Core.src
{
    
    public class ResourcesAssetProvider : IAssetProvider
    {
        public async UniTask<T> ProvideAsset<T>(string assetKey, CancellationToken cancellationToken) where T : Object
        {
            var loadedResource = Resources.LoadAsync<GameObject>(assetKey);

            var completionSource = new TaskCompletionSource<bool>();
            loadedResource.completed += (asyncOperation) =>
            {
                completionSource.SetResult(asyncOperation.isDone);
            };
            await completionSource.Task;
            
            var castedGameObject = loadedResource.asset as GameObject;
            return castedGameObject.GetComponent<T>();
        }

        public UniTask<bool> IsAssetLoaded(string assetKey, CancellationToken cancellationToken)
        {
            return IsAssetLoadedUniTaskImp(assetKey, cancellationToken);
        }
        
        private UniTask<bool> IsAssetLoadedUniTaskImp(string assetKey, CancellationToken cancellationToken)
        {
            return UniTask.FromResult(true);
        }

        public void ReleaseAsset<T>(T loadedAsset) where T : Object
        {
            Resources.UnloadAsset(loadedAsset);
        }
    }
    
}