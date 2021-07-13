using System.Threading;
using UnityEngine;

#if UNITASK_ENABLED
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace Core.src
{
    public class ResourcesAssetProvider : IAssetProvider
    {
        
#if UNITASK_ENABLED
        public async UniTask<T> ProvideAsset<T>(string assetKey, CancellationToken cancellationToken) where T : Object
        
#else
        public async Task<T> ProvideAsset<T>(string assetKey, CancellationToken cancellationToken) where T : Object
#endif
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

        public void ReleaseAsset<T>(T loadedAsset) where T : Object
        {
            Resources.UnloadAsset(loadedAsset);
        }
    }
}