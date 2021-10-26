using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.src.Utils
{
    public class SceneHelper : ISceneHelper
    {
        private GameObject cachedInstance;
        
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

        public SceneHelper()
        {
            SceneManager.sceneLoaded += (arg0, mode) => { cachedInstance = null; };
        }
    }
}