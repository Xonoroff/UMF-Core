using Core.src.Utils;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class GameObjectFactory : IGameObjectFactory
    {
        private readonly DiContainer container;

        private readonly ISceneHelper sceneHelper;
        
        public GameObjectFactory(DiContainer container,
            ISceneHelper sceneHelper)
        {
            this.container = container;
            this.sceneHelper = sceneHelper;
        }

        public T CreateFromComponentOnPrefab<T>(T componentOnPrefab, bool dontDestroy = true) where T : MonoBehaviour
        {
            var instantiated = container.InstantiatePrefab(componentOnPrefab);
            if (!dontDestroy)
            {
                instantiated.transform.SetParent(sceneHelper.Root.transform);
            }
            
            return instantiated.GetComponent<T>();
        }

        public TView CreateFromView<TView, TViewEntity>(TView view, TViewEntity viewEntity, bool dontDestroy = true) where TView : IView<TViewEntity>
        {
            var instantiated = container.InstantiatePrefab(view.ViewGameObject);
            var viewComponent = instantiated.GetComponent<TView>();
            viewComponent.SetViewEntity(viewEntity);
            if (!dontDestroy)
            {
                instantiated.transform.SetParent(sceneHelper.Root.transform);
            }
            
            return viewComponent;
        }
    }
}