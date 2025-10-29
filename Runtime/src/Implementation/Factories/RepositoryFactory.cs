using System;
using System.Collections.Concurrent;
using UMF.Core.Infrastructure;
using Zenject;

namespace UMF.Core.Implementation
{
    /// <summary>
    /// Default repository factory that can create repositories by id for any data type <typeparamref name="T"/>.
    /// Uses Zenject container to construct repositories with their dependencies.
    /// </summary>
    public sealed class RepositoryFactory : IRepositoryFactory
    {
        private readonly DiContainer container;

        // Cache created repositories per (data type, id)
        private readonly ConcurrentDictionary<(Type DataType, RepositoryId Id), object> cacheByEnum = new();
        private readonly ConcurrentDictionary<(Type DataType, string Id), object> cacheByString = new();

        public RepositoryFactory(DiContainer container)
        {
            this.container = container;
        }

        public IRepository<T> Get<T>(RepositoryId id)
        {
            var key = (typeof(T), id);
            if (cacheByEnum.TryGetValue(key, out var existing))
            {
                return (IRepository<T>)existing;
            }

            var repo = CreateByEnum<T>(id);
            cacheByEnum[key] = repo;
            return repo;
        }

        public IRepository<T> Get<T>(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var normalized = id.Trim();
            var key = (typeof(T), normalized);
            if (cacheByString.TryGetValue(key, out var existing))
            {
                return (IRepository<T>)existing;
            }

            var repo = CreateByString<T>(normalized);
            cacheByString[key] = repo;
            return repo;
        }

        private IRepository<T> CreateByEnum<T>(RepositoryId id)
        {
            switch (id)
            {
                case RepositoryId.Session:
                {
                    var type = typeof(SessionRepository<>).MakeGenericType(typeof(T));
                    return (IRepository<T>)container.Instantiate(type);
                }
                case RepositoryId.PlayerPrefs:
                {
                    var type = typeof(PlayerPrefsRepository<>).MakeGenericType(typeof(T));
                    return (IRepository<T>)container.Instantiate(type);
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(id), id, "Unsupported repository id");
                }
            }
        }

        private IRepository<T> CreateByString<T>(string id)
        {
            // Match known ids (case-insensitive)
            if (id.Equals(RepositoryIds.Session, StringComparison.OrdinalIgnoreCase))
            {
                return CreateByEnum<T>(RepositoryId.Session);
            }

            if (id.Equals(RepositoryIds.PlayerPrefs, StringComparison.OrdinalIgnoreCase))
            {
                return CreateByEnum<T>(RepositoryId.PlayerPrefs);
            }

            // Allow also enum names
            if (Enum.TryParse<RepositoryId>(id, true, out var parsed))
            {
                return CreateByEnum<T>(parsed);
            }

            throw new ArgumentException($"Unknown repository id: '{id}'", nameof(id));
        }
    }
}
