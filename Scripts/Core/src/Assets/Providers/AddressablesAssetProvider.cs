using System.Threading;
using System.Threading.Tasks;
#if UNITASK_ENABLED
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

using UnityEngine;


namespace Core.src
{
    #if UNITY_ADDRESSABLES
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;
    
    public class AddressablesAssetProvider : IAssetProvider
    {
     
#if UNITASK_ENABLED
        public UniTask<T> ProvideAsset<T>(string assetKey, CancellationToken cancellationToken) where T : Object
        {
            return Addressables.LoadAssetAsync<T>(assetKey).ToUniTask(cancellationToken: cancellationToken);
        }

        public async UniTask<bool> IsAssetLoaded(string assetKey, CancellationToken cancellationToken)
        {
            var downloadSize = await GetDownloadSize(assetKey).ToUniTask(cancellationToken: cancellationToken);
            return downloadSize == 0;
        }
#else
        public Task<T> ProvideAsset<T>(string assetKey, CancellationToken cancellationToken) where T : Object
        {
            return Addressables.LoadAssetAsync<T>(assetKey).Task;
        }
        
        public async Task<bool> IsAssetLoaded(string assetKey, CancellationToken cancellationToken)
        {
            var downloadSize = await GetDownloadSize(assetKey).Task;
            return downloadSize == 0;
        }
#endif

        private AsyncOperationHandle<long> GetDownloadSize(string assetKey)
        {
            return Addressables.GetDownloadSizeAsync(assetKey);
        }
        
        public void ReleaseAsset<T>(T loadedAsset) where T : Object
        {
            Addressables.Release(loadedAsset);
        }
    }
    
    #endif
}