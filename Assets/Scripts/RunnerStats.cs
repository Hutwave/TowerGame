using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New runner", menuName = "Runner Creation / Runner unit")]

public class RunnerStats : ScriptableObject
{

    public string runnerType;
    public float speed;
    public float defense;
    public float maxHealth;
    public Material newColor;
    private float actualSpeed;
    private float actualMaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        randomizeStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void randomizeStats()
    {
        actualSpeed = Random.Range(speed * 0.17f, speed * 1.03f);
        actualMaxHealth = Random.Range(maxHealth * 0.95f, maxHealth);
    }
}
