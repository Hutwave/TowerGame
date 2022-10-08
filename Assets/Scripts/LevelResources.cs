using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelResources : MonoBehaviour
{
    public int startingMoney = 10110;
    int currentMoney;
    public int CurrentMoney { get { return currentMoney; } }


    private Base baseObject;

    public TextMeshProUGUI displayMoney;

    private void Awake()
    {
        baseObject = GetComponent<Base>();
        currentMoney = startingMoney;
        UpdateDisplay();
    }

    public void GainMoney(int amount)
    {
        currentMoney += amount;
        UpdateDisplay();
    }

    public void LoseMoney(int amount)
    {
        currentMoney -= amount;
        UpdateDisplay();
    }

    public void LoseLife(int amount)
    {
        baseObject.loseLives(amount);
    }

    public void GainLife(int amount)
    {
        baseObject.loseLives(-amount);
    }

    void UpdateDisplay()
    {
        displayMoney.text = $"Money: {currentMoney}";
    }

}
