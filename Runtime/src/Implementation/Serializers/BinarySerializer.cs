using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UMF.Core.Infrastructure;

namespace UMF.Core.Implementation.Serializers
{
    /// <summary>
    ///     BinaryFormatter-based implementation of <see cref="IPolymorphicSerializer{TBuffer}" /> with <c>TBuffer = byte[]</c>
    ///     .
    ///     Note: BinaryFormatter is obsolete; use only for trusted in-memory deep copies.
    /// </summary>
    public sealed class BinarySerializer<TObject> : ISerializer<byte[], TObject>
    {
        public byte[] SerializeObject(TObject value)
        {
            if (ReferenceEquals(value, null))
            {
                return Array.Empty<byte>();
            }

            var type = value.GetType();
            if (!type.IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", nameof(value));
            }

            using (var ms = new MemoryStream())
            {
#pragma warning disable SYSLIB0011 // BinaryFormatter is obsolete in modern .NET but allowed here for trusted deep copy
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, value);
#pragma warning restore SYSLIB0011
                return ms.ToArray();
            }
        }

        public TObject DeserializeObject(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.Length == 0)
            {
                return default;
            }

            using (var ms = new MemoryStream(data))
            {
#pragma warning disable SYSLIB0011
                var formatter = new BinaryFormatter();
                return (TObject)formatter.Deserialize(ms);
#pragma warning restore SYSLIB0011
            }
        }
    }
}