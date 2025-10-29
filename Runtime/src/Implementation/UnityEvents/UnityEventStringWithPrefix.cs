using System;
using UnityEngine;

namespace UMF.Core.Implementation
{
    [Serializable]
    public class UnityEventStringWithPrefix
    {
        public void Invoke(string value)
        {
            unityEvent?.Invoke(prefix + value);
        }
#pragma warning disable 0649
        [SerializeField] private string prefix;

        [SerializeField] private UnityEventString unityEvent;
#pragma warning restore 0649
    }
}