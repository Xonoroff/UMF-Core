namespace UMF.Core.Infrastructure
{
    public class SignalBaseWithParameter<T> : SignalBase
    {
        public SignalBaseWithParameter(T model)
        {
            Model = model;
        }

        public T Model { get; private set; }
    }
}