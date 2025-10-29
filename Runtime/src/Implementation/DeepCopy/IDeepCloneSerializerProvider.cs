using UMF.Core.Infrastructure;

namespace UMF.Core.Implementation.DeepCopy
{
    /// <summary>
    ///     Provides serializers suitable for deep cloning objects via a buffer representation.
    ///     Returns an <see cref="ISerializer{TResult,TObject}" /> where <c>TResult</c> is <see cref="byte[]" />.
    /// </summary>
    public interface IDeepCloneSerializerProvider
    {
        ISerializer<byte[], TObject> GetSerializer<TObject>();
    }
}