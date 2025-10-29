namespace UMF.Core.Infrastructure
{
    /// <summary>
    /// String identifiers for repositories to use with <see cref="IRepositoryFactory"/>.
    /// Prefer <see cref="RepositoryId"/> where possible; these are provided for configuration-driven lookups.
    /// </summary>
    public static class RepositoryIds
    {
        public const string Session = "session";
        public const string PlayerPrefs = "playerPrefs";
    }
}
