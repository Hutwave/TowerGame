using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int maxHitPoints;
    public int currentHitPoints;

    Enemy enemy;

    /*
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }
    */

    void Start()
    {
        enemy = GetComponent<Enemy>();
        currentHitPoints = maxHitPoints;
    }

    public void TakeDamage(int damage)
    {
        currentHitPoints -= damage;

        if (gameObject.activeInHierarchy && currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
            enemy.RewardMoney();
        }
    }


}
