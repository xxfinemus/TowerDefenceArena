using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraFade_Script : MonoBehaviour
{
    Image fader;

    [SerializeField]
    float fadeSpeed;

    bool sceneStarting;

    bool arenaSceneEnding;
    bool tdSceneEnding;
    bool sceneEnding;

    public bool SceneEnding
    {
        get { return sceneEnding; }
        set { sceneEnding = value; }
    }

    [SerializeField]
    string levelToLoad;

    public bool ArenaSceneEnding
    {
        get { return arenaSceneEnding; }
        set { arenaSceneEnding = value; }
    }

    public bool SceneStarting
    {
        get { return sceneStarting; }
        set { sceneStarting = value; }
    }
    public bool TDSceneEnding
    {
        get { return tdSceneEnding; }
        set { tdSceneEnding = value; }
    }

    public string LevelToLoad
    {
        get { return levelToLoad; }
        set { levelToLoad = value; }
    }

    void Awake()
    {
        sceneStarting = true;        
        tdSceneEnding = false;

        fader = GetComponentInChildren<Image>();

        if (fader != null)
        {
            fader.color = Color.black;

            fader.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        }
    }

    void Update()
    {
        if (sceneStarting)
        {
            StartScene();
        }

        if (sceneEnding)
        {
            EndScene();
        }
        if (tdSceneEnding)
        {
            EndTDScene();
        }
        if (ArenaSceneEnding)
        {
            EndArenaScene();
        }
    }

    /// <summary>
    /// Called at the staTrt of the scene
    /// </summary>
    void StartScene()
    {
        if (fader != null)
        {
            FadeToClear();

            if (fader.color.a <= 0.05f)
            {
                fader.color = Color.clear;
                fader.enabled = false;
                sceneStarting = false;
            }
        }
    }

    /// <summary>
    /// Call this to end the scene, fade to black and load a new scene
    /// </summary>
    public void EndTDScene()
    {
        if (fader != null)
        {
            //Garbage test code delete later or decide to keep it so you only have to call this function once to start the fadeout
            tdSceneEnding = true;

            fader.enabled = true;

            FadeToBlack();

            if (fader.color.a >= 0.95f)
            {
                fader.color = Color.black;

                PhaseChange.EnterArena();
                sceneStarting = true;
            }
        }
    }
    public void EndScene()
    {
        if (fader != null)
        {
            //Garbage test code delete later or decide to keep it so you only have to call this function once to start the fadeout
            tdSceneEnding = true;

            fader.enabled = true;

            FadeToBlack();

            if (fader.color.a >= 0.95f)
            {
                fader.color = Color.black;

                Application.LoadLevel(levelToLoad);
            }
        }
    }
    public void EndArenaScene()
    {
        if (fader != null)
        {
            //Garbage test code delete later or decide to keep it so you only have to call this function once to start the fadeout
            tdSceneEnding = true;

            fader.enabled = true;

            FadeToBlack();

            if (fader.color.a >= 0.95f)
            {
                fader.color = Color.black;

                PhaseChange.EnterTD();
                sceneStarting = true;
            }
        }
    }

    void FadeToClear()
    {
        fader.color = Color.Lerp(fader.color, Color.clear, Time.deltaTime * fadeSpeed);
    }

    void FadeToBlack()
    {
        fader.color = Color.Lerp(fader.color, Color.black, Time.deltaTime * fadeSpeed);
    }
}
