using System;

namespace Core.src.Messaging
{
    public class EventBusRequest<TResponse>
    {
        public Action<TResponse> Callback { get; set; }
    }
}