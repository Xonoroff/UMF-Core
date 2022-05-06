using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core.src.Utils
{
    public class MonoBehaviourEventWrapperComponent : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onAwakeUnityEvent;
        
        [SerializeField]
        private UnityEvent onDestroyUnityEvent;

        public event Action OnAwakeEvent;
        
        public event Action OnDestroyEvent;
        
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
    }
}