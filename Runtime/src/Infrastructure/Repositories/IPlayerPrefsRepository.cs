using System;

namespace UMF.Core.Infrastructure
{
    [Obsolete("Use IRepositoryFactory to obtain IRepository<T> instances. This interface will be removed in a future release.")]
    public interface IPlayerPrefsRepository<T> : IRepository<T>
    {
    }
}