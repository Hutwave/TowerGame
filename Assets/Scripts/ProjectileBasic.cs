using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBasic : MonoBehaviour
{
    public float speed = 70f;

    private Transform target;
    private int damage;

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    public void setDamage(int _damage)
    {
        damage = _damage;
    }

    private void HitTarget()
    {
        EnemyHealth hp = target.GetComponent<EnemyHealth>();
        hp.TakeDamage(damage);
        Destroy(gameObject);
        return;
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

}
