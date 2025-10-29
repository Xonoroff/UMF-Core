using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UMF.Core.Infrastructure
{
    public interface IAssetProvider
    {
        UniTask<T> ProvideAsset<T>(string assetKey, CancellationToken cancellationToken) where T : Object;

        UniTask<bool> IsAssetLoaded(string assetKey, CancellationToken cancellationToken);

        void ReleaseAsset<T>(T loadedAsset) where T : Object;
    }
}