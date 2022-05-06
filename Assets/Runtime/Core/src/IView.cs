using UnityEngine;

namespace DefaultNamespace
{
    public interface IView<TViewEntity>
    {
        GameObject ViewGameObject { get; }
        
        TViewEntity ViewEntity { get; }

        void SetViewEntity(TViewEntity viewEntity);
    }
}