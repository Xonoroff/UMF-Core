namespace Core.src.Infrastructure
{
    public interface ISettable<in TEntity>
    {
        void Set(TEntity entity);
    }
}