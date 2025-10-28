namespace UMF.Core.Infrastructure
{
    public interface IReward
    {
        string RewardId { get; set; }

        string RewardCategory { get; set; }

        ulong Amount { get; set; }

        ulong ExpiredAt { get; set; }
    }
}