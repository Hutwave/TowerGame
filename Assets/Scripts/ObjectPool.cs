using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public GameObject runnerPrefab;
    public GameObject runner2Prefab;
    public int poolSize = 5;
    public float spawnTimer = 1f;

    GameObject[] pool;

    void Awake()
    {
    }

    void Start()
    {
        StartCoroutine("PopulatePool");
        StartCoroutine("SpawnRunner", 5);
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(runnerPrefab, transform);
            pool[i].SetActive(false);
        }
        pool[4] = Instantiate(runner2Prefab, transform);
    }

    void EnableObjectInPool()
    {
        for(int i = 0; i < pool.Length; i++)
        {
            if(pool[i].activeInHierarchy == false)
            {
                var asd = pool[i].GetComponent<RunnerHealth>();
                asd.currentHitPoints = asd.maxHitPoints;
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnRunner()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

}
