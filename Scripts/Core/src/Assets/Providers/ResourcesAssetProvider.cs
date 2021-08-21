
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

#if UNITASK_ENABLED
using Cysharp.Threading.Tasks;
#endif

namespace Core.src
{
    
    public class ResourcesAssetProvider : IAssetProvider
    {
        public async
#if UNITASK_ENABLED
            UniTask<T>
#else
            Task<T>
#endif
            ProvideAsset<T>(string assetKey, CancellationToken cancellationToken) where T : Object
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

        public
#if UNITASK_ENABLED
        UniTask<bool>
#else
        Task<bool>
#endif
            IsAssetLoaded(string assetKey, CancellationToken cancellationToken)
        {
#if UNITASK_ENABLED
            return IsAssetLoadedUniTaskImp(assetKey, cancellationToken);
#else
            return IsAssetLoadedTaskImp(assetKey, cancellationToken);
#endif
        }
        
#if UNITASK_ENABLED
        private UniTask<bool> IsAssetLoadedUniTaskImp(string assetKey, CancellationToken cancellationToken)
        {
            return UniTask.FromResult(true);
        }
#else
        private Task<bool> IsAssetLoadedTaskImp(string assetKey, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
#endif
        
        public void ReleaseAsset<T>(T loadedAsset) where T : Object
        {
            Resources.UnloadAsset(loadedAsset);
        }
    }
    
}