using UnityEngine;
using System.Collections;

public class StatScript : MonoBehaviour
{
    private int exp;
    private int gold;
    private int points;

    private int strength;
    private int vitality;
    private int speed;

    #region Properties
    public int Points
    float exp, gold, points, enemiesLeaked, bossHealth;


    public float Points
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
    public float EnemiesLeaked
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

    // Use this for initialization
    void Start()
    {
        LoadStats();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadStats()
    {

<<<<<<< HEAD
        exp = PlayerPrefs.GetInt("exp", exp);
        gold = PlayerPrefs.GetInt("gold", gold);
        points = PlayerPrefs.GetInt("points", points);

        strength = PlayerPrefs.GetInt("strength", strength);
        vitality = PlayerPrefs.GetInt("vitality", vitality);
        speed = PlayerPrefs.GetInt("speed", speed);

        //Debug.Log("Exp: " + PlayerPrefs.GetInt("exp") + "; Gold: " + PlayerPrefs.GetInt("gold") + "; Points: " + PlayerPrefs.GetInt("points") + "; Strength: " + PlayerPrefs.GetInt("strength"));
=======
        exp = PlayerPrefs.GetFloat("exp");
        gold = PlayerPrefs.GetFloat("gold");
        points = PlayerPrefs.GetFloat("points");
        enemiesLeaked = PlayerPrefs.GetFloat("enemiesLeaked");
        bossHealth = PlayerPrefs.GetFloat("bossHealth");

        Debug.Log("Exp loaded: " + PlayerPrefs.GetFloat("exp") +
            "Gold loaded: " + PlayerPrefs.GetFloat("gold") +
            "Points loaded: " + PlayerPrefs.GetFloat("points") +
            "Enemies leaked loaded: " + PlayerPrefs.GetFloat("enemiesLeaked") +
            "Boss health loaded: " + PlayerPrefs.GetFloat("bossHealth"));
>>>>>>> 739382bfb6b3559e007f2571d90ee42df7b0beaf

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

<<<<<<< HEAD
        //Debug.Log("Exp: " + PlayerPrefs.GetInt("exp") + "; Gold: " + PlayerPrefs.GetInt("gold") + "; Points: " + PlayerPrefs.GetInt("points") + "; Strength: " + PlayerPrefs.GetInt("strength"));
=======
        Debug.Log("Exp saved: " + PlayerPrefs.GetFloat("exp") +
            "Gold saved: " + PlayerPrefs.GetFloat("gold") +
            "Points saved: " + PlayerPrefs.GetFloat("points") +
            "Enemies leaked saved: " + PlayerPrefs.GetFloat("enemiesLeaked") +
            "Boss health saved: " + PlayerPrefs.GetFloat("bossHealth"));
>>>>>>> 739382bfb6b3559e007f2571d90ee42df7b0beaf
    }

    /// <summary>
    /// Increases or decreases the selected stat by the given value (To decrease give a negative number)
    /// </summary>
    /// <param name="stat">The name of the stat "exp", "gold", "points", "enemy", "enemyhealth"</param>
    /// <param name="value">The numerical value to change the stat by</param>
    public void ChangeStat(string stat, int value)
    {
        switch (stat)
        {
            case "exp":
                exp += value;
                break;

            case "gold":
                gold += value;
                break;

            case "points":
                points += value;
                break;

<<<<<<< HEAD
            case "strength":
                strength += value;
                break;

            case "vitality":
                vitality += value;
                break;

            case "speed":
                speed += value;
                break;
        }

        // Debug.Log(stat + " changed by " + value);
    }

    /// <summary>
    /// Returns the value if the specified stat. If the stat does not exists 0 is returned
    /// </summary>
    /// <param name="stat">The stat which value is returned</param>
    /// <returns></returns>
    public int GetStat(string stat)
    {
        int value;
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
=======
            case "enemy":
                enemiesLeaked += value;
                break;

            case "enemyHealth":
                bossHealth += value;
>>>>>>> 739382bfb6b3559e007f2571d90ee42df7b0beaf
                break;
        }
        return value;
    }

    /// <summary>
    /// Sets the chosen stat to be equal to the given value
    /// </summary>
    /// <param name="stat">"exp", "gold", "points", "enemy", "enemyhealth"</param>
    /// <param name="value">The value you want the stat to be</param>
    public void SetStat(string stat, int value)
    {
        switch (stat)
        {
            case "exp":
                exp = value;
                break;

            case "gold":
                gold = value;
                break;

            case "points":
                points = value;
                break;

<<<<<<< HEAD
            case "strength":
                strength = value;
                break;

            case "vitality":
                vitality = value;
                break;

            case "speed":
                speed = value;
                break;


=======
            case "enemy":
                enemiesLeaked = value;
                break;

            case "enemyHealth":
                bossHealth = value;
                break;

>>>>>>> 739382bfb6b3559e007f2571d90ee42df7b0beaf
            default:
                Debug.Log(stat + "does not exist");
                break;
        }
        // Debug.Log(stat + " set to " + value);
    }

    /// <summary>
    /// Resets all the stat to their default values (0)
    /// </summary>
    public void SetStatsToDefault()
    {
        exp = 0;
        gold = 0;
        points = 0;
        strength = 0;
        vitality = 0;
        speed = 0;

        SaveStats();
    }

    /// <summary>
    /// Resets all saved stats to 0. Use with caution
    /// </summary>
    public void ResetStats()
    {
        PlayerPrefs.SetFloat("exp", 0);

        PlayerPrefs.SetFloat("gold", 0);

        PlayerPrefs.SetFloat("points", 0);

        PlayerPrefs.SetFloat("enemiesLeaked", 0);

        PlayerPrefs.SetFloat("bossHealth", 0);

        PlayerPrefs.Save();
    }
}
