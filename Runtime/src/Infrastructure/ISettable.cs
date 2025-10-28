namespace UMF.Core.Infrastructure
{
    public interface ISettable<in TEntity>
    {
        void Set(TEntity entity);
    }
}