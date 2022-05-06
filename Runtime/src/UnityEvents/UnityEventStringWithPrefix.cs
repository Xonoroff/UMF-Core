using System;
using UnityEngine;

namespace Core.src.UnityEvents
{
    [Serializable]
    public class UnityEventStringWithPrefix
    {
#pragma warning disable 0649
        [SerializeField]
        private string prefix;

        [SerializeField]
        private UnityEventString unityEvent;
#pragma warning restore 0649
        
        public void Invoke(string value)
        {
            unityEvent?.Invoke(prefix + value);
        }

    }
}