using System.Threading;
using UnityEngine;

using Cysharp.Threading.Tasks;

namespace Core.src
{
    public interface IAssetLoader
    {
        UniTask<T> LoadAssetAsync<T>(string assetKey, CancellationToken cancellationToken) where T : Object;
        
        void ReleaseAllLoadedAssets();
    }
}