using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerHealth : MonoBehaviour
{

    public float maxHitPoints;
    public float currentHitPoints;
    public float armor;
    private float checkEverySec;

    Runner runner;

    // Status ailments
    private float poison;
    private float poisonWait;

    private float slow;
    private float slowWait;
    private float slowLast;

    private float fire;
    private float fireWait;

    private float weakened;
    private float weakenedWait;

    private float regen;
    private float regenWait;

    private float stun;
    private float stunWait;
    private float stunImmunity;

    private float hardened;
    private float hardenedWait;

    public float poisonResist;
    public float burnResist;
    public float slowResist;
    public float stunResist;
    public float regenResist;
    public float hardenedResist;
    public float weakenedResist;


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

    public void TakeDamage(float damage)
    {
        float damageToBeTaken = damage * (0.01f * weakened + 1f);
        damageToBeTaken /= (1 + (Mathf.Log10(Mathf.Sqrt(2 * armor)) * (0.90f + 0.05f * hardened)));
        currentHitPoints -= damageToBeTaken;

        if (gameObject.activeInHierarchy && currentHitPoints <= 0)
        {
            poison = 0;
            slow = 0;
            fire = 0;
            weakened = 0;
            gameObject.SetActive(false);
            runner.RewardMoney();
        }
    }

    void Update()
    {
        checkEverySec += Time.deltaTime;
        if (checkEverySec > 1f)
        {
            checkEverySec -= 1f;
            // SLOW checking from RunnerMover
            if (poison > 0)
            {
                applyPoison();
            }
            if (fire > 0)
            {
                applyFire();
            }
            if (regen > 0)
            {
                applyRegen();
            }
            if (weakened > 0)
            {
                applyWeakened();
            }
        }
        
    }

    public void addPoison(float poisonAmt)
    {
        if(poison == 0 || 0.85f * poisonAmt > poison)
        {
            Debug.LogError("new");
            poison = 0.9f * poisonAmt * (1-(0.01f*poisonResist));
            poisonWait = 5f;
        }
        else if(poisonAmt*3.3f - poison > 0)
        {
            if(poisonResist == 0) { poisonResist = 2f; }
            Debug.Log("Pois:" + poison + " amt:" + poisonAmt + " and" + (3f*0.5f*poisonResist).ToString());
            poison = Mathf.Sqrt((poisonAmt*poisonAmt+poison*poison)/(3f*0.5f*poisonResist));
            poisonWait = 5f*(1f-0.01f*poisonResist);
        }
        else
        {
            poisonWait = 5f * (1f - 0.01f * poisonResist);
        }
    }

    public void addSlow(float slowAmt)
    {
        Debug.Log("AMT " + slowAmt);
        if (slow < (0.95f * (slowAmt / 102)))
        {
            slowWait = 2f;
            slow = 0.95f * (slowAmt / 100);
            Debug.Log("AA " + slow);
        }

        else if (slowWait < 30)
        {
            slowWait += 1;
        }
        else
        {
            slowWait = 30;
        }

        var testRaise = -0.9 + Mathf.Log10(0.69f * slowAmt + 4.4f);
        if (testRaise > slow)
        {
            slow += (slowAmt / 1000) * (slowWait / (50 + 3 * slowAmt));
        }
        
    }

    public void addFire(float fireAmt)
    {
        fire += fireAmt;
    }

    public void addRegen(float regenAmt)
    {
        regen = regenAmt;
    }

    public void applyPoison()
    {
        Debug.Log(poison);
        Debug.Log(poison * (0.01f * weakened + 1f * Time.deltaTime));
        currentHitPoints -= poison * (0.01f * weakened);
        Debug.Log(currentHitPoints);
        if (poisonWait > -10f)
        {
            poisonWait -= 1f;
        }
        if(poisonWait < 0f)
        {
            poison -= Mathf.Abs(poison*(0.02f * poisonWait));
        }
    }
    public float applySlow()
    {
        slowLast = slowWait;
        if (slowWait == 0)
        {
            return 0;
        }
        if(slowWait < 0.5f)
        {
            slowWait = 0;
            slow = 0;
            return 0;
        }

        if(slowWait < slowLast)
        {
            slowWait = Mathf.Floor(slowWait * 0.92f - 0.35f);
        }
        if (slowWait < 20 && slowWait < slowLast)
        {
            slow *= 0.7f + (slowWait / 80) * slow;
        }
        Debug.Log("ACTUAL: " + slow + " " + slowWait);
        return slow;
    }
    public void applyFire()
    {

    }
    public void applyRegen()
    {

    }
    public void applyWeakened()
    {

    }


}
