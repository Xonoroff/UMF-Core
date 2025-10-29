namespace UMF.Core.Infrastructure
{
    /// <summary>
    /// Provider interface for obtaining a polymorphic serializer instance.
    /// </summary>
    /// <typeparam name="TBuffer">Buffer type used by the serializer (e.g., byte[], string).</typeparam>
    public interface IPolymorphicSerializerProvider<TBuffer>
    {
        IPolymorphicSerializer<TBuffer> GetSerializer();
    }
}
