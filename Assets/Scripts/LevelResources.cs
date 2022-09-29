using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelResources : MonoBehaviour
{

    public int startingMoney = 10110;
    int currentMoney;
    public int CurrentMoney { get { return currentMoney; } }

    public TextMeshProUGUI displayMoney;

    private void Awake()
    {
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

    void UpdateDisplay()
    {
        displayMoney.text = $"Money: {currentMoney}";
    }

}
