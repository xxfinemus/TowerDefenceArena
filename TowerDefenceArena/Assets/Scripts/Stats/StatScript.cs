using UnityEngine;
using System.Collections;

public class StatScript : MonoBehaviour
{
    float exp;

    float gold;

    float points;

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

        exp = PlayerPrefs.GetFloat("exp", exp);
        gold = PlayerPrefs.GetFloat("gold", gold);
        points = PlayerPrefs.GetFloat("points", points);

        Debug.Log(PlayerPrefs.GetFloat("exp") + PlayerPrefs.GetFloat("gold") + PlayerPrefs.GetFloat("points"));

    }

    public void SaveStats()
    {
        PlayerPrefs.SetFloat("exp", exp);

        PlayerPrefs.SetFloat("gold", gold);

        PlayerPrefs.SetFloat("points", points);

        PlayerPrefs.Save();

        Debug.Log(PlayerPrefs.GetFloat("exp") + PlayerPrefs.GetFloat("gold") + PlayerPrefs.GetFloat("points"));
    }

    /// <summary>
    /// Increases or decreases the selected stat by the given value (To decrease give a negative number)
    /// </summary>
    /// <param name="stat">The name of the stat "exp", "gold", "points"</param>
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
        }
    }

    /// <summary>
    /// Sets the chosen stat to be equal to the given value
    /// </summary>
    /// <param name="stat">"exp", "gold", "points"</param>
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

            default:
                Debug.Log(stat + "does not exist");
                break;
        }
        Debug.Log(stat + "changed by" + value);
    }
}
