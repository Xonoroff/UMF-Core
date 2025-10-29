namespace UMF.Core.Infrastructure
{
    public interface ISerializer<TResult, TObject>
    {
        TResult SerializeObject(TObject value);

        TObject DeserializeObject(TResult value);
    }
}