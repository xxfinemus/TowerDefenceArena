using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayStats : MonoBehaviour
{
    [SerializeField]
    private string stat;

    private GameObject world;
    private StatScript stats;
    private Text display;
    
    // Use this for initialization
    void Start()
    {
        display = GetComponent<Text>();
        world = GameObject.Find("World");

        stats = world.GetComponent<StatScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.GetStat(stat).ToString() != display.text)
        {
            display.text = stats.GetStat(stat).ToString();
        }
    }
}
