namespace Core.src.Infrastructure
{
    public interface ISyncService
    {
        void SyncTo<T>(T value);

        T SyncFrom<T>();
    }
}