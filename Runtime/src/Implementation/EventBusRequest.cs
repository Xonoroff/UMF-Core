using System;

namespace UMF.Core.Implementation
{
    public class EventBusRequest<TResponse>
    {
        public Action<TResponse> Callback { get; set; }
    }
}