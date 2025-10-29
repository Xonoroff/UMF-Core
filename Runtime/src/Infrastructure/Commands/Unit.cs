namespace UMF.Core.Infrastructure
{
    /// <summary>
    /// A void-like value type to use for commands that do not produce a meaningful result.
    /// </summary>
    public readonly struct Unit
    {
        public static readonly Unit Value = new Unit();
    }
}
