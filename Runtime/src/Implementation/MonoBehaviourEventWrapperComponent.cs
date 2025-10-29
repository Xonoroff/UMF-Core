using System;
using UnityEngine;
using UnityEngine.Events;

namespace UMF.Core.Implementation
{
    public class MonoBehaviourEventWrapperComponent : MonoBehaviour
    {
        private UnityEvent onAwakeUnityEvent;

        private UnityEvent onDestroyUnityEvent;

        public virtual void Awake()
        {
            onAwakeUnityEvent?.Invoke();
            OnAwakeEvent?.Invoke();
        }

        public void OnDestroy()
        {
            onDestroyUnityEvent?.Invoke();
            OnDestroyEvent?.Invoke();
        }

        public event Action OnAwakeEvent;

        public event Action OnDestroyEvent;
    }
}