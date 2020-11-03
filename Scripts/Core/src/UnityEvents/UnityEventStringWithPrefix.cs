using System;
using UnityEngine;

namespace Core.src.UnityEvents
{
    [Serializable]
    public class UnityEventStringWithPrefix
    {
        [SerializeField]
        private string prefix;

        [SerializeField]
        private UnityEventString unityEvent;

        public void Invoke(string value)
        {
            unityEvent?.Invoke(prefix + value);
        }

    }
}