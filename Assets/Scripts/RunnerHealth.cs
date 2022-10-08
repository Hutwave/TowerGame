using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerHealth : MonoBehaviour
{

    public int maxHitPoints;
    public int currentHitPoints;

    Runner runner;

    /*
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }
    */

    void Start()
    {
        runner = GetComponent<Runner>();
        currentHitPoints = maxHitPoints;
    }

    public void TakeDamage(int damage)
    {
        currentHitPoints -= damage;

        if (gameObject.activeInHierarchy && currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
            runner.RewardMoney();
        }
    }


}
