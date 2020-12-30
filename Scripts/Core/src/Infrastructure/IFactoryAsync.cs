using System.Threading.Tasks;

namespace Scripts.Core.src.Infrastructure
{
    public interface IFactoryAsync<T>
    {
#if UNITASK_ENABLED
         Cysharp.Threading.Tasks.UniTask<T> CreateAsync();
#else
        Task<T> CreateAsync();
#endif
    }
}
