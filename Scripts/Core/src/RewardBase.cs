namespace MF.Core.Scripts.Core.src
{
    public class RewardBase : IReward
    {
        public string RewardId { get; set; }
        
        public string RewardCategory { get; set; }
        
        public ulong Amount { get; set; }
        
        public ulong ExpiredAt { get; set; }
    }
}