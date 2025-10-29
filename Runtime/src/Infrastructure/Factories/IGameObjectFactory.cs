using UnityEngine;

namespace UMF.Core.Infrastructure
{
    public interface IGameObjectFactory
    {
        T CreateFromComponentOnPrefab<T>(T componentOnPrefab, bool dontDestroy = true) where T : MonoBehaviour;

        TView CreateFromView<TView, TViewEntity>(TView view, TViewEntity viewEntity, bool dontDestroy = true)
            where TView : IView<TViewEntity>;
    }
}