using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float buildDelay = 0.2f;
    [SerializeField] int towerSpace = 50;

    void Start()
    {
        StartCoroutine(Build());
    }

    public int getSpace()
    {
        return towerSpace;
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        
        LevelResources bank = FindObjectOfType<LevelResources>();

        if (bank == null)
        {
            return false;
        }

        if (bank.CurrentMoney >= cost)
        {
            Instantiate(tower, position, Quaternion.identity);
            bank.LoseMoney(cost);
            return true;
        }

        return false;
    }

    IEnumerator Build()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
    }
}
