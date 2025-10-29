using System.Threading;
using Cysharp.Threading.Tasks;

namespace UMF.Core.Infrastructure
{
    public interface IFactoryAsync<T>
    {
        UniTask<T> CreateAsync(CancellationToken cts = default);
    }
}