using System.Threading;
using UnityEngine;

using Cysharp.Threading.Tasks;

namespace Core.src
{
    public interface IAssetProvider
    {
        UniTask<T> ProvideAsset<T>(string assetKey, CancellationToken cancellationToken) where T : Object;

        UniTask<bool> IsAssetLoaded(string assetKey, CancellationToken cancellationToken);

        void ReleaseAsset<T>(T loadedAsset) where T : Object;
    }
}