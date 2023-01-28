using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{

    public int towerSize;
    public int goldPrice;

    public int poison;
    public int slow;
    public int fire;
    public int weakened;
    public int hardened;
    public int regen;

    public int bulletDamage;


    public int GetDamage()
    {
        return bulletDamage;
    }

    public Dictionary<statusEnums, int> GetStatuses()
    {
        var statusList = new Dictionary<statusEnums, int>();
        if (poison > 0)
        {
            statusList.Add(statusEnums.Poison, poison);
        }
        if (fire > 0)
        {
            statusList.Add(statusEnums.Fire, fire);
        }
        if (slow > 0)
        {
            statusList.Add(statusEnums.Slow, slow);
        }
        if (regen > 0)
        {
            statusList.Add(statusEnums.Regen, regen);
        }
        if (weakened > 0)
        {
            statusList.Add(statusEnums.Weakened, weakened);
        }
        if (hardened > 0)
        {
            statusList.Add(statusEnums.Hardened, hardened);
        }
        return statusList;
    }

}
