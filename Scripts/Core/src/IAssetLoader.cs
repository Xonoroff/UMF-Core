using System.Threading;
using UnityEngine;

#if UNITASK_ENABLED
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace Core.src
{
    public interface IAssetLoader
    {
#if UNITASK_ENABLED
        UniTask<T> LoadAssetAsync<T>(string assetKey, CancellationToken cancellationToken) where T : Object;
#else 
        Task<T> LoadAssetAsync<T>(string assetKey, CancellationToken cancellationToken) where T : Object;
#endif 
        
        void ReleaseAllLoadedAssets();
    }
}