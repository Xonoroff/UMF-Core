namespace UMF.Core.Infrastructure
{
    public interface IKeysProvider<TObject, TResult>
    {
        TResult ProvideKey(TObject value);
    }
}