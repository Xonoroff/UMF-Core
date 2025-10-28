using UnityEngine;

namespace UMF.Core.Infrastructure
{
    public interface IView<TViewEntity>
    {
        GameObject ViewGameObject { get; }

        TViewEntity ViewEntity { get; }

        void SetViewEntity(TViewEntity viewEntity);
    }
}