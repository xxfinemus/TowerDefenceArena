using UnityEngine;
using System.Collections;

public class TowerVars : MonoBehaviour {

    [SerializeField]
    private int cost;
    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private int maxLevel;
    [SerializeField]
    private int rangeUpgradeStat;
    [SerializeField]
    private int damageUpgradeStat;

    public int DamageUpgradeStat
    {
        get
        {
            return damageUpgradeStat;
        }

        set
        {
            damageUpgradeStat = value;
        }
    }
    public int RangeUpgradeStat
    {
        get
        {
            return rangeUpgradeStat;
        }

        set
        {
            rangeUpgradeStat = value;
        }
    }
    public int Cost
    {
        get
        {
            return cost;
        }
    }
    public int MaxLevel
    {
        get
        {
            return maxLevel;
        }

        set
        {
            maxLevel = value;
        }
    }
    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }

        set
        {
            currentLevel = value;
        }
    }
}
