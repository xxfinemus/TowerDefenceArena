using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    private static bool gamePaused;

    public static bool GamePaused
    {
        get { return Pause.gamePaused; }
    }
	// Use this for initialization
	void Start () 
    {
        gamePaused = false;
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    public static void StartPause()
    {
        Time.timeScale = 0;
        gamePaused = true;
    }

    public static void EndPause()
    {
        Time.timeScale = 1;
        gamePaused = false;
    }
}
