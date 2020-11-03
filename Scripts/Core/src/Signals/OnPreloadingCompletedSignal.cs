namespace Core.src.Signals
{
    public class OnPreloadingCompletedSignal : SignalBaseWithParameter<bool>
    {
        public OnPreloadingCompletedSignal(bool model) : base(model)
        {
            
        }
    }
}