namespace Core.src
{
    public interface IRepository<TData>
    {
        TData Get();

        void Update(TData data);
    }
}