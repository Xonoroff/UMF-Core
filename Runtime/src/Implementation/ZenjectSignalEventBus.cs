using System;
using Cysharp.Threading.Tasks;
using UMF.Core.Infrastructure;
using Zenject;

namespace UMF.Core.Implementation
{
    public class ZenjectSignalEventBus : IEventBus
    {
        private readonly SignalBus signalBus;

        public ZenjectSignalEventBus(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        public UniTask<TResponse> FireAsync<TRequest, TResponse>(TRequest request)
            where TRequest : EventBusRequest<TResponse>
        {
            var taskCompletionSource = new UniTaskCompletionSource<TResponse>();

            void Handler(TResponse response)
            {
                request.Callback -= Handler;
                taskCompletionSource.TrySetResult(response);
            }

            request.Callback += Handler;

            signalBus.Fire(request);
            return taskCompletionSource.Task;
        }

        public void DeclareSignal<TSignal>()
        {
            signalBus.DeclareSignal<TSignal>();
        }

        public void Fire<TRequest>(TRequest request)
        {
            signalBus.Fire(request);
        }

        public void Subscribe<TRequest>(Action callback)
        {
            signalBus.Subscribe<TRequest>(callback);
        }

        public void Subscribe<TRequest>(Action<TRequest> callback)
        {
            signalBus.Subscribe<TRequest>(callback);
        }

        public void Unsubscribe<TRequest>(Action callback)
        {
            signalBus.Unsubscribe<TRequest>(callback);
        }

        public void Unsubscribe<TRequest>(Action<TRequest> callback)
        {
            signalBus.Unsubscribe<TRequest>(callback);
        }
    }
}