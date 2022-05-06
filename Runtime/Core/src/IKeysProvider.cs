namespace MF.Core.Scripts.Core.src
{
    public interface IKeysProvider<TObject, TResult>
    {
        TResult ProvideKey(TObject value);
    }
}