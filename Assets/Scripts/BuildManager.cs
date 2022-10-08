using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public List<GameObject> units;
    private GameObject turretToBuild;
    public List<GameObject> turrets;
    public static BuildManager instance;
    private LevelResources bank;

    private Transform spawnPoint;

    private void Awake()
    {
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        bank = FindObjectOfType<LevelResources>();
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    private void Start()
    {
        
    }

    public void UnselectTower()
    {
        turretToBuild = null;
    }

    public GameObject GetTurretToBuild()
    {
        if(turretToBuild != null)
        {
            //Debug.Log(turretToBuild.name);
        }
        return turretToBuild;
    }

    public void SetTurretToBuild(string towerName)
    {
        var foundTurret = turrets.Find(x => x.name.ToLower() == towerName.ToLower());

        if(foundTurret != null)
        {
            turretToBuild = foundTurret;
        }
    }

    public void purchaseUnit(string unitName)
    {
        var foundUnit = units.Find(x => x.name.ToLower() == unitName.ToLower());
        if(foundUnit != null) {
            UnitStats stats = foundUnit.GetComponent<UnitStats>();
            if(bank.CurrentMoney >= stats.goldPrice)
            {
                bank.LoseMoney(stats.goldPrice);
                Instantiate(foundUnit, spawnPoint);
            }
        }
    }


}
