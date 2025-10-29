using System;
using System.Threading;
using System.Threading.Tasks;
using UMF.Core.Infrastructure;
using UnityEngine;

namespace UMF.Core.Implementation
{
    /// <summary>
    /// Handles execution of <see cref="UnloadUnusedResources"/> by invoking Unity's Resources.UnloadUnusedAssets asynchronously.
    /// </summary>
    public sealed class UnloadUnusedResourcesHandler : ICommandHandler<UnloadUnusedResources, Unit>
    {
        public async Task<Unit> ExecuteAsync(
            UnloadUnusedResources command,
            CancellationToken cancellationToken = default,
            IProgress<float> progress = null)
        {
            // Unity returns AsyncOperation which we can poll for completion.
            AsyncOperation op = Resources.UnloadUnusedAssets();
            while (!op.isDone)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    // Unity doesn't support cancelling the operation, but we can stop waiting and report current progress.
                    progress?.Report(op.progress);
                    break;
                }

                progress?.Report(op.progress);
                // Yield to the Unity player loop while we wait.
                await Task.Yield();
            }

            progress?.Report(1f);
            return Unit.Value;
        }
    }
}
