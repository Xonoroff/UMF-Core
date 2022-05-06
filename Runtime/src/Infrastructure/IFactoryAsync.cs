using System.Threading;

namespace Scripts.Core.src.Infrastructure
{
    public interface IFactoryAsync<T>
    {
         Cysharp.Threading.Tasks.UniTask<T> CreateAsync(CancellationToken cts = default);
    }
}
