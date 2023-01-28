using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public GameObject[] runnerPrefabs;
    public int poolSize;
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
            pool[i] = Instantiate(runnerPrefabs[0], transform);
            pool[i].SetActive(false);
        }
        if(pool.Length>0)pool[pool.Length-1] = Instantiate(runnerPrefabs[1], transform);
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
