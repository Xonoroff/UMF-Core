using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.src
{
    public interface IAssetLoader
    {
        UniTask<T> LoadAssetAsync<T>(string assetKey, CancellationToken cancellationToken) where T : Object;
        
        void ReleaseAllLoadedAssets();
    }
}