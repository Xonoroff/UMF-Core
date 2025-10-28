using System.Collections.Generic;

namespace UMF.Core.Infrastructure
{
    public interface IRewardBox
    {
        string Id { get; set; }

        List<IReward> Rewards { get; set; }
    }
}