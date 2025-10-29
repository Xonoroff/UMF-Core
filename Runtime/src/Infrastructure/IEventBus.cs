using System;
using Cysharp.Threading.Tasks;
using UMF.Core.Implementation;

namespace UMF.Core.Infrastructure
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
        UniTask<TResponse> FireAsync<TRequest, TResponse>(TRequest request)
            where TRequest : EventBusRequest<TResponse>;
    }

}