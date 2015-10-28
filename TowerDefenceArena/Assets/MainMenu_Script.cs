using UnityEngine;
using System.Collections;

public class MainMenu_Script : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScene(string scene)
    {
        Debug.Log("Loading scene:" + scene);
        Application.LoadLevel(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
