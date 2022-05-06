using System;
using UnityEngine.Networking;

namespace Core.src.Infrastructure
{
    public interface ICoroutineManager
    {
        void Wait(UnityWebRequest webRequest, Action callback);
    }
}