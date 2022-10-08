using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{

    public int moneyReward = 25;
    public int moneyPenalty = 50;

    LevelResources levelResources;

    // Start is called before the first frame update
    void Start()
    {
        levelResources = FindObjectOfType<LevelResources>();
    }

    public void RewardMoney()
    {
        if(levelResources != null)
        {
            levelResources.GainMoney(moneyReward);
        }
    }

    public void PenaltyMoney()
    {
        if (levelResources != null)
        {
            //levelResources.LoseMoney(moneyPenalty);
            levelResources.LoseLife(1);
        }
    }
}
