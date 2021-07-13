using System.Threading;
using UnityEngine;

#if UNITASK_ENABLED
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace Core.src
{
    public interface IAssetProvider
    {
#if UNITASK_ENABLED
        UniTask<T> ProvideAsset<T>(string assetKey, CancellationToken cancellationToken) where T : Object;
#else
        Task<T> ProvideAsset<T>(string assetKey, CancellationToken cancellationToken) where T : Object;
#endif
        

        void ReleaseAsset<T>(T loadedAsset) where T : Object;
    }
}