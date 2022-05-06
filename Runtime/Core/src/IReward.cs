using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReward
{
    string RewardId { get; set; }
    
    string RewardCategory { get; set; }
    
    ulong Amount { get; set; }
        
    ulong ExpiredAt { get; set; }
}
