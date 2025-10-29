using System;
using System.Collections.Generic;
using System.Linq;
using UMF.Core.Infrastructure;
using UnityEngine;

namespace UMF.Core.Implementation
{
    [Serializable]
    public class RewardsContainer : ISerializationCallbackReceiver
    {
        private readonly Dictionary<string, List<IReward>> rewards = new();

        [SerializeField] private readonly List<RewardsContainerSerializationWrapper> serializableRewards = new();

        public void OnBeforeSerialize()
        {
            serializableRewards.Clear();
            foreach (var reward in rewards)
                serializableRewards.Add(new RewardsContainerSerializationWrapper
                {
                    Key = reward.Key,
                    Rewards = reward.Value.Select(x => x as RewardBase).ToList()
                });
        }

        public void OnAfterDeserialize()
        {
            foreach (var serializableReward in serializableRewards)
                rewards.Add(serializableReward.Key, serializableReward.Rewards.Select(x => x as IReward).ToList());
        }

        public bool ContainsKey(string general)
        {
            return rewards.ContainsKey(general);
        }

        public List<IReward> GetRewards(string key)
        {
            return rewards[key];
        }

        public void Add(string rewardRewardCategory, List<IReward> list)
        {
            rewards.Add(rewardRewardCategory, list);
        }

        [Serializable]
        public class RewardsContainerSerializationWrapper
        {
            public string Key;

            public List<RewardBase> Rewards;
        }
    }
}