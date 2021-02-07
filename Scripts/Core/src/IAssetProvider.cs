using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.src
{
    public interface IAssetProvider
    {
        UniTask<T> ProvideAsset<T>(string assetKey, CancellationToken cancellationToken) where T : Object;

        void ReleaseAsset<T>(T loadedAsset) where T : Object;
    }
}