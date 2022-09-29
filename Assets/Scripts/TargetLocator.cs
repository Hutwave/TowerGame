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

    bool turning = false;
    float towerRange;
    private bool inRange = false;
    private float cooldown;

    private void Start()
    {
        cooldown = 0.2f;
        InvokeRepeating("GetClosestEnemy", 0f, 1f);
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

    void GetClosestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        towerRange = 200f;
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Enemy potentialTarget in enemies)
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
