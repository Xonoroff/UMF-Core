using System;
using UnityEngine;

namespace MF.Core.Scripts.Core.src
{
    [Serializable]
    public class RewardBase : IReward
    {
        [field: SerializeField]
        public string Id { get; set; }

        [field: SerializeField]
        public string RewardId { get; set; }

        [field: SerializeField]
        public string RewardCategory { get; set; }
        
        [field: SerializeField]
        public ulong Amount { get; set; }
        
        [field: SerializeField]
        public ulong ExpiredAt { get; set; }
    }
}