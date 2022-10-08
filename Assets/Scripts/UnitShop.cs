using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShop : MonoBehaviour
{

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void PurchaseUnit(string unitName)
    {
        buildManager.purchaseUnit(unitName);
    }   
}
