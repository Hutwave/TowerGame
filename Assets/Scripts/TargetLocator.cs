using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    public Transform weapon;
    public Transform target;
    public bool particleShooting;
    public GameObject bulletProjectile;
    public float initialCooldown;
    public float towerRange;

    bool turning = false;
    private bool inRange = false;
    private float cooldown;

    private void Start()
    {
        cooldown = 0.2f;
        InvokeRepeating("GetClosestRunner", 0f, 1f);
    }
    void Update()
    {
        if (cooldown > 0f)
        {
            cooldown = cooldown - Time.deltaTime;
        }
        if(target == null)
        {
            return;
        }
        AimWeapon();
        if (!particleShooting && cooldown < 0f && inRange)
        {
            Shoot();
        }
    }

    void GetClosestRunner()
    {
        Runner[] enemies = FindObjectsOfType<Runner>();
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Runner potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr && dSqrToTarget < towerRange*1.05f)  
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
                inRange = dSqrToTarget < towerRange;
            }
        }
        target = bestTarget;
    }

    void AimWeapon()
    {
        weapon.LookAt(new Vector3(target.position.x, particleShooting? target.position.y : weapon.position.y, target.position.z));
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletProjectile, weapon.position, weapon.rotation);
        ProjectileBasic bullet = bulletGO.GetComponent<ProjectileBasic>();
        bullet.setDamage(2);
        bullet.Seek(target);
        cooldown = initialCooldown;
    }
}
