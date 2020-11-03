using Core.src.Messaging;
using Core.src.Signals;
using UnityEngine;
using Zenject;

namespace Core.src.Utils
{
    public class ApplicationCycleTracker : MonoBehaviour
    {
        [Inject]
        private IEventBus signalBus;

        private void OnApplicationQuit()
        {
            signalBus.Fire(new OnApplicationQuitSignal());
        }
    }
}