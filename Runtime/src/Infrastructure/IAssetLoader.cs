using Cysharp.Threading.Tasks;

namespace UMF.Core.Infrastructure
{
    public interface IAssetLoader
    {
        UniTask<T> LoadAssetAsync<T>(string assetKey, CancellationToken cancellationToken) where T : Object;

        void ReleaseAllLoadedAssets();
    }
}