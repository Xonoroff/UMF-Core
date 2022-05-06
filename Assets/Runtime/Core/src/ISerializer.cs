namespace MF.Core.Scripts.Core.src
{
    public interface ISerializer<TResult, TObject>
    {
        
        TResult SerializeObject(TObject value);
        
        TObject DeserializeObject(TResult value);
    }
}