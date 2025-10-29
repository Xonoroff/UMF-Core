using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UMF.Core.Infrastructure
{
    public interface IAssetLoader
    {
        UniTask<T> LoadAssetAsync<T>(string assetKey, CancellationToken cancellationToken) where T : Object;

        void ReleaseAllLoadedAssets();
    }
}