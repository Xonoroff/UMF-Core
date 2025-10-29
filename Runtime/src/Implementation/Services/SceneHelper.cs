using UMF.Core.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UMF.Core.Implementation
{
    public class SceneHelper : ISceneHelper
    {
        private GameObject cachedInstance;

        public SceneHelper()
        {
            SceneManager.sceneLoaded += (arg0, mode) => { cachedInstance = null; };
        }

        public GameObject Root
        {
            get
            {
                if (cachedInstance == null)
                {
                    cachedInstance = new GameObject("SceneRoot");
                }

                return cachedInstance;
            }
        }
    }
}