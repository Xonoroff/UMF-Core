using UMF.Core.Implementation.Serializers;
using UMF.Core.Infrastructure;

namespace UMF.Core.Implementation.DeepCopy
{
    /// <summary>
    ///     Default provider that returns a new <see cref="BinarySerializer{TObject}" /> per request.
    ///     Uses BinaryFormatter under the hood for trusted in-memory deep copies.
    /// </summary>
    public sealed class DeepCopySerializerProvider : IDeepCloneSerializerProvider
    {
        public ISerializer<byte[], TObject> GetSerializer<TObject>()
        {
            return new BinarySerializer<TObject>();
        }
    }
}