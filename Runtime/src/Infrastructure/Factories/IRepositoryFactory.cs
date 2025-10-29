using System;
using UMF.Core.Infrastructure;

namespace UMF.Core.Infrastructure
{
    /// <summary>
    /// Factory that returns an <see cref="IRepository{TData}"/> implementation by identifier.
    /// Use <see cref="RepositoryId"/> or string ids from <see cref="RepositoryIds"/>.
    /// </summary>
    public interface IRepositoryFactory
    {
        IRepository<T> Get<T>(RepositoryId id);
        IRepository<T> Get<T>(string id);
    }
}
