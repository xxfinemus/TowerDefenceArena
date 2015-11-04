using UnityEngine;
using System.Collections;

public class StatScript : MonoBehaviour
{
    float exp, gold, points, enemiesLeaked, bossHealth;


    public float Points
    {
        get { return points; }
    }
    public float Gold
    {
        get { return gold; }
    }
    public float Exp
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

    }

    public void SaveStats()
    {
        PlayerPrefs.SetFloat("exp", exp);

        PlayerPrefs.SetFloat("gold", gold);

        PlayerPrefs.SetFloat("points", points);

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
                exp += value;
                break;

            case "gold":
                gold += value;
                break;

            case "points":
                points += value;
                break;

            case "enemy":
                enemiesLeaked += value;
                break;

            case "enemyHealth":
                bossHealth += value;
                break;
        }
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
                exp = value;
                break;

            case "gold":
                gold = value;
                break;

            case "points":
                points = value;
                break;

            case "enemy":
                enemiesLeaked = value;
                break;

            case "enemyHealth":
                bossHealth = value;
                break;

            default:
                Debug.Log(stat + "does not exist");
                break;
        }
        Debug.Log(stat + "changed by" + value);
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
