using UnityEngine;
using System.Collections;

public class StatScript : MonoBehaviour
{
    private static StatScript instance;

    private int exp;
    private int gold;
    private int points;

    private int strength;
    private int vitality;
    private int speed;

    int enemiesLeaked;
    float bossHealth;
    #region Properties

    public static StatScript Instance
    {
        get { return instance ?? (instance = new GameObject("StatScriptObj").AddComponent<StatScript>()); }
    }
    public int Points
    {
        get { return points; }
    }

    public int Gold
    {
        get { return gold; }
    }

    public int Exp
    {
        get { return exp; }
    }

    public int EnemiesLeaked
    {
        get { return enemiesLeaked; }
    }

    public float BossHealth
    {
        get { return bossHealth; }
    }

    public int Strength
    {
        get { return strength; }
    }

    public int Vitality
    {
        get { return vitality; }
    }

    public int Speed
    {
        get { return speed; }
    }
    #endregion

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
        DontDestroyOnLoad(this);
        
    }
    // Use this for initialization
    void Start()
    {
        LoadStats();
        ChangeStat("gold", 2000);
    }
    
    public void LoadStats()
    {
        exp = PlayerPrefs.GetInt("exp");
        gold = PlayerPrefs.GetInt("gold");
        points = PlayerPrefs.GetInt("points");
        enemiesLeaked = PlayerPrefs.GetInt("enemiesLeaked");
        bossHealth = PlayerPrefs.GetFloat("bossHealth");

        strength = PlayerPrefs.GetInt("strength", strength);
        vitality = PlayerPrefs.GetInt("vitality", vitality);
        speed = PlayerPrefs.GetInt("speed", speed);

        Debug.Log("Exp loaded: " + PlayerPrefs.GetFloat("exp") +
            "Gold loaded: " + PlayerPrefs.GetFloat("gold") +
            "Points loaded: " + PlayerPrefs.GetFloat("points") +
            "Enemies leaked loaded: " + PlayerPrefs.GetFloat("enemiesLeaked") +
            "Boss health loaded: " + PlayerPrefs.GetFloat("bossHealth"));

    }



    public void SaveStats()
    {
        PlayerPrefs.SetInt("exp", exp);

        PlayerPrefs.SetInt("gold", gold);

        PlayerPrefs.SetInt("points", points);

        PlayerPrefs.SetInt("strength", strength);

        PlayerPrefs.SetInt("vitality", vitality);

        PlayerPrefs.SetInt("speed", speed);

        PlayerPrefs.SetFloat("enemiesLeaked", enemiesLeaked);

        PlayerPrefs.SetFloat("bossHealth", bossHealth);


        PlayerPrefs.Save();

        Debug.Log("Exp saved: " + PlayerPrefs.GetFloat("exp") +
            "Gold saved: " + PlayerPrefs.GetFloat("gold") +
            "Points saved: " + PlayerPrefs.GetFloat("points") +
            "Enemies leaked saved: " + PlayerPrefs.GetFloat("enemiesLeaked") +
            "Boss health saved: " + PlayerPrefs.GetFloat("bossHealth"));
    }

    /// <summary>
    /// Increases or decreases the selected stat by the given value (To decrease give a negative number)
    /// </summary>
    /// <param name="stat">The name of the stat "exp", "gold", "points", "enemy", "enemyhealth"</param>
    /// <param name="value">The numerical value to change the stat by</param>
    public void ChangeStat(string stat, float value)
    {
        switch (stat)
        {
            case "exp":
                exp += (int)value;
                break;

            case "gold":
                gold += (int)value;
                break;

            case "points":
                points += (int)value;
                break;

            case "enemy":
                enemiesLeaked += (int)value;
                break;

            case "bossHealth":
                bossHealth += value;
                break;

            case "strength":
                strength += (int)value;
                break;

            case "vitality":
                vitality += (int)value;
                break;

            case "speed":
                speed += (int)value;
                break;
        }

        // Debug.Log(stat + " changed by " + value);
    }

    /// <summary>
    /// Returns the value if the specified stat. If the stat does not exists 0 is returned
    /// </summary>
    /// <param name="stat">The stat which value is returned</param>
    /// <returns></returns>
    public float GetStat(string stat)
    {
        float value;
        switch (stat)
        {
            case "exp":
                value = exp;
                break;

            case "gold":
                value = gold;
                break;

            case "points":
                value = points;
                break;

            case "enemy":
                value = enemiesLeaked;
                break;

            case "bossHealth":
                value = bossHealth;
                break;

            case "strength":
                value = strength;
                break;

            case "vitality":
                value = vitality;
                break;

            case "speed":
                value = speed;
                break;

            default:
                value = 0;
                break;

        }
        return value;
    }

    /// <summary>
    /// Sets the chosen stat to be equal to the given value
    /// </summary>
    /// <param name="stat">"exp", "gold", "points", "enemy", "enemyhealth"</param>
    /// <param name="value">The value you want the stat to be</param>
    public void SetStat(string stat, float value)
    {
        switch (stat)
        {
            case "exp":
                exp = (int)value;
                break;

            case "gold":
                gold = (int)value;
                break;

            case "points":
                points = (int)value;
                break;

            case "enemy":
                enemiesLeaked = (int)value;
                break;

            case "enemyHealth":
                bossHealth = value;
                break;

            case "strength":
                strength = (int)value;
                break;

            case "vitality":
                vitality = (int)value;
                break;

            case "speed":
                speed = (int)value;
                break;

            default:
                Debug.Log(stat + "does not exist");
                break;
        }
        // Debug.Log(stat + " set to " + value);
    }

    /// <summary>
    /// Resets all the stat to their default values (0). Use with caution
    /// </summary>
    public void SetStatsToDefault()
    {
        exp = 0;
        gold = 0;
        points = 0;
        enemiesLeaked = 0;
        bossHealth = 0;
        strength = 0;
        vitality = 0;
        speed = 0;

        SaveStats();
    }

}
