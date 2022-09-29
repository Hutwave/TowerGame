using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public GameObject enemyPrefab;
    public int poolSize = 5;
    public float spawnTimer = 1f;

    GameObject[] pool;

    void Awake()
    {
    }

    void Start()
    {
        StartCoroutine("PopulatePool");
        StartCoroutine("SpawnEnemy", 5);
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
        }
    }

    void EnableObjectInPool()
    {
        for(int i = 0; i < pool.Length; i++)
        {
            if(pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

}
