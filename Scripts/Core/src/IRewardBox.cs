using System.Collections.Generic;

namespace MF.Core.Scripts.Core.src
{
    public interface IRewardBox
    {
        public string Id { get; set; }
        
        public List<IReward> Rewards { get; set; }
    }
}