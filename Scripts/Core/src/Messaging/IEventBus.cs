using System;
using System.Threading.Tasks;

namespace Core.src.Messaging
{
    public interface IEventBus
    {
        void Fire<TRequest>(TRequest request);
        
        void Subscribe<TRequest>(Action callback);
        
        void Subscribe<TRequest>(Action<TRequest> callback);

        void Unsubscribe<TRequest>(Action callback);

        void Unsubscribe<TRequest>(Action<TRequest> callback);
        
        Task<TResponse> FireAsync<TRequest, TResponse>(TRequest request)
            where TRequest : EventBusRequest<TResponse>;
    }
}