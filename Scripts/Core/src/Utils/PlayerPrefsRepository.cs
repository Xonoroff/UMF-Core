using System;
using MF.Core.Scripts.Core.src;
using Scripts.Core.src.Infrastructure;
using UnityEngine;

namespace Core.src.Utils
{
    public class PlayerPrefsRepository<T> : IPlayerPrefsRepository<T> where T : class
    {
        private readonly IKeysProvider<Type, string> keysProvider;

        private readonly ISerializer<string, T> serializer;

        private readonly IFactorySync<T> objectFactory;

        private T cachedData;

        public PlayerPrefsRepository(IKeysProvider<Type, string> keysProvider,
            IFactorySync<T> objectFactory,
            ISerializer<string, T> serializer)
        {
            this.keysProvider = keysProvider;
            this.objectFactory = objectFactory;
            this.serializer = serializer;
            
            var key = keysProvider.ProvideKey(typeof(T));
            var existedPrefs = PlayerPrefs.GetString(key);
            var deserialize = serializer.DeserializeObject(existedPrefs);
            if (deserialize != null)
            {
                cachedData = deserialize;
            }
            else
            {
                var newObject = objectFactory.Create();
                var serializedObject = serializer.SerializeObject(newObject);
                cachedData = newObject;

                PlayerPrefs.SetString(key, serializedObject);
            }
        }

        public T Get()
        {
            return cachedData;
        }

        public void Update(T data)
        {
            cachedData = data;

            var key = keysProvider.ProvideKey(typeof(T));
            var serialized = serializer.SerializeObject(data);

            PlayerPrefs.SetString(key, serialized);
        }
    }
}