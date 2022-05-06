using Core.src.Messaging;
using Core.src.Signals;
using UnityEngine;
using Zenject;

namespace Core.src.Utils
{
    public class ApplicationCycleTracker : MonoBehaviour
    {
#pragma warning disable 0649
        [Inject]
        private IEventBus signalBus;
#pragma warning restore 0649
        
        private void OnApplicationQuit()
        {
            signalBus?.Fire(new OnApplicationQuitSignal());
        }
    }
}