using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class UpgradeHero : MonoBehaviour
{
    [SerializeField]
    private GameObject heroMenu;
    [SerializeField]
    private Text priceTag;
    [SerializeField]
    private Text totalCostText;
    [SerializeField]
    private Text strengthText;
    [SerializeField]
    private Text vitalityText;
    [SerializeField]
    private Text speedText;
    [SerializeField]
    private int upgradeCost = 100;

    private int tempExp;
    private int tempStrength;
    private int currentStrength;
    private int tempVitality;
    private int currentVitality;
    private int tempSpeed;
    private int currentSpeed;

    private StatScript stats;

    private void Start()
    {
        stats = GameObject.Find("World").GetComponent<StatScript>();

        priceTag.text = "Cost per Upgrade: " + upgradeCost + " EXP";
    }

    private void Update()
    {
        strengthText.text = currentStrength.ToString();

        vitalityText.text = currentVitality.ToString();

        speedText.text = currentSpeed.ToString();

        totalCostText.text = "Total Cost: " + ((currentStrength - tempStrength + currentVitality - tempVitality + currentSpeed - tempSpeed) * upgradeCost).ToString() + " EXP";


        if (Input.GetKeyDown(KeyCode.K))
        {
            stats.ChangeStat("exp", upgradeCost);
        }
    }

    public void OpenHeroUpgrade()
    {
        // Pause the game here
        Pause.StartPause();

        heroMenu.SetActive(true);

        tempExp = stats.Exp;

        tempStrength = stats.Strength;
        currentStrength = tempStrength;

        tempVitality = stats.Vitality;
        currentVitality = tempVitality;

        tempSpeed = stats.Speed;
        currentSpeed = tempSpeed;
    }

    public void CloseHeroUpgrade()
    {
        // Unpause the game here
        Pause.EndPause();

        heroMenu.SetActive(false);
    }

    public void ConfirmChanges()
    {
        stats.SetStat("exp", tempExp);
        stats.SetStat("strength", Convert.ToInt32(strengthText.text));
        stats.SetStat("vitality", Convert.ToInt32(vitalityText.text));
        stats.SetStat("speed", Convert.ToInt32(speedText.text));

        CloseHeroUpgrade();
    }

    public void AddOneLevel(string stat)
    {
        if (tempExp >= upgradeCost)
        {
            switch (stat)
            {
                case "strength":
                    currentStrength += 1;
                    tempExp -= upgradeCost;
                    break;

                case "vitality":
                    currentVitality += 1;
                    tempExp -= upgradeCost;
                    break;

                case "speed":
                    currentSpeed += 1;
                    tempExp -= upgradeCost;
                    break;

                default:
                    break;
            }
        }
    }

    public void SubtractOneLevel(string stat)
    {
        switch (stat)
        {
            case "strength":
                if (currentStrength > tempStrength)
                {
                    currentStrength -= 1;
                    tempExp += upgradeCost;
                }
                break;

            case "vitality":
                if (currentVitality > tempVitality)
                {
                    currentVitality -= 1;
                    tempExp += upgradeCost;
                }
                break;

            case "speed":
                if (currentSpeed > tempSpeed)
                {
                    currentSpeed -= 1;
                    tempExp += upgradeCost;
                }
                break;

            default:
                break;
        }
    }
}
