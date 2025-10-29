using System;
using UMF.Core.Infrastructure;

namespace UMF.Core.Implementation.DeepCopy
{
    public static class DeepCopy
    {
        /// <summary>
        ///     Global provider for obtaining an <see cref="IDeepCloneSerializer" /> used by the parameterless
        ///     <see cref="DeepClone{T}(T)" />.
        ///     Set this to integrate a different serializer (e.g., MessagePack, JSON, DataContractSerializer, etc.).
        ///     Defaults to <see cref="DeepCopySerializerProvider" />.
        /// </summary>
        private static IDeepCloneSerializerProvider SerializerProvider { get; } = new DeepCopySerializerProvider();

        /// <summary>
        ///     Creates a deep clone of an object using the serializer from <see cref="SerializerProvider" />.
        /// </summary>
        public static T DeepClone<T>(this T obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return default;
            }

            var provider = SerializerProvider ??
                           throw new InvalidOperationException("DeepCopy.SerializerProvider is not set.");
            var serializer = provider.GetSerializer<T>() ??
                             throw new InvalidOperationException("SerializerProvider returned null serializer.");
            return DeepClone(obj, serializer);
        }

        /// <summary>
        ///     Creates a deep clone of an object using a supplied <see cref="IDeepCloneSerializer" />.
        /// </summary>
        public static T DeepClone<T>(this T obj, ISerializer<byte[], T> serializer)
        {
            if (ReferenceEquals(obj, null))
            {
                return default;
            }

            if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            var serialized = serializer.SerializeObject(obj);
            var result = serializer.DeserializeObject(serialized);
            return result;
        }
    }
}