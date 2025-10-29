using UMF.Core.Infrastructure;
using UnityEngine;

namespace UMF.Core.Implementation
{
    public class ApplicationCycleTracker : MonoBehaviour
    {
        private IEventBus signalBus;

        public void Initialize(IEventBus eventBus)
        {
            signalBus = eventBus;
        }
        
        private void OnApplicationQuit()
        {
            signalBus?.Fire(new OnApplicationQuitSignal());
        }
    }
}