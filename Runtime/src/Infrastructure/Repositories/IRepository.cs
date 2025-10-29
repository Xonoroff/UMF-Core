namespace UMF.Core.Infrastructure
{
    public interface IRepository<TData>
    {
        TData Get();

        void Update(TData data);
    }
}