using System;
using System.Threading.Tasks;

#if UNITASK_ENABLED
using Cysharp.Threading.Tasks;
#endif

namespace Core.src.Messaging
{
    public interface IEventBus : IEventBusSync, IEventBusAsync
    {
        void DeclareSignal<TSignal>();
    }
    
    public interface IEventBusSync
    {
        void Fire<TRequest>(TRequest request);
        
        void Subscribe<TRequest>(Action callback);
        
        void Subscribe<TRequest>(Action<TRequest> callback);

        void Unsubscribe<TRequest>(Action callback);

        void Unsubscribe<TRequest>(Action<TRequest> callback);
    }

    public interface IEventBusAsync
    {
        #if UNITASK_ENABLED
        UniTask<TResponse> FireAsyncUniTask<TRequest, TResponse>(TRequest request)
            where TRequest : EventBusRequest<TResponse>;
        #endif

        Task<TResponse> FireAsync<TRequest, TResponse>(TRequest request)
            where TRequest : EventBusRequest<TResponse>;
    }
}