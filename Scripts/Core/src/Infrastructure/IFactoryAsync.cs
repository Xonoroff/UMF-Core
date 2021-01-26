using System.Threading;
using System.Threading.Tasks;

namespace Scripts.Core.src.Infrastructure
{
    public interface IFactoryAsync<T>
    {
#if UNITASK_ENABLED
         Cysharp.Threading.Tasks.UniTask<T> CreateAsync(CancellationToken cts = default);
#else
        Task<T> CreateAsync(CancellationToken cts = default);
#endif
    }
}
