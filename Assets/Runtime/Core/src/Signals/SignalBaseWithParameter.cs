namespace Core.src.Signals
{
    public class SignalBaseWithParameter<T> : SignalBase
    {
        public T Model { get; private set; }
        
        public SignalBaseWithParameter(T model)
        {
            Model = model;
        }
    }
}