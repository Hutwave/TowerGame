using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{

    public int startLife;

    private int currentLife;

    // Start is called before the first frame update
    void Start()
    {
        currentLife = startLife;
    }

    public void loseLives(int amount)
    {
        currentLife -= amount;
        if(currentLife < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("DIED LOL NOOB");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
