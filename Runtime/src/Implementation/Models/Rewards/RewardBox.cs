using System.Collections.Generic;
using UMF.Core.Infrastructure;

namespace UMF.Core.Implementation
{
    public class RewardBox : IRewardBox
    {
        public string Id { get; set; }

        public List<IReward> Rewards { get; set; }
    }
}