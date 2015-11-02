using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour {

    [SerializeField]
    private List<GameObject> objectsToDisable;

    [SerializeField]
    private List<GameObject> objectsToEnable;

    [SerializeField]
    private List<GameObject> hsname;
    [SerializeField]
    private List<GameObject> hsscore;
    [SerializeField]
    private List<GameObject> hslevel;

    private dbscript dbscript;


    void OnEnable()
    {
        object[,] scores = dbscript.GetTopScores();
        for (int i = 0; i < 10; i++)
        {
            hsname[i].GetComponent<Text>().text = scores[i, 0].ToString();
            hslevel[i].GetComponent<Text>().text = scores[i, 2].ToString();
            hsscore[i].GetComponent<Text>().text = scores[i, 1].ToString();
        }
        foreach (GameObject item in objectsToDisable)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in objectsToEnable)
        {
            item.SetActive(true);
        }
    }
    void OnDisable()
    {
        foreach (GameObject item in objectsToDisable)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in objectsToEnable)
        {
            item.SetActive(false);
        }
    }
}
