namespace UMF.Core.Infrastructure
{
    public interface IFactorySync<T>
    {
        T Create();
    }
}