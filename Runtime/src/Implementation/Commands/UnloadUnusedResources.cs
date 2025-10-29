using UMF.Core.Infrastructure;

namespace UMF.Core.Implementation
{
    /// <summary>
    /// Command that requests unloading of unused Unity resources.
    /// Use with <see cref="UnloadUnusedResourcesHandler"/>.
    /// </summary>
    public sealed class UnloadUnusedResources : ICommand<Unit>
    {
        // No payload needed for now. Extend with options if necessary.
    }
}
