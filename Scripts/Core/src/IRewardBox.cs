using System.Collections.Generic;

namespace MF.Core.Scripts.Core.src
{
    public interface IRewardBox
    {
        string Id { get; set; }
        
        List<IReward> Rewards { get; set; }
    }
}