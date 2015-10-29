// The object this script is attached to must have a child with a text component
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonMute : MonoBehaviour
{
    [SerializeField]
    private Camera myCamera;
    private AudioListener listener;
    private Text buttonText;

    // Use this for initialization
    void Start()
    {
        listener = myCamera.GetComponent<AudioListener>();
        buttonText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (listener.enabled)
        {
            buttonText.text = "Mute";
            Debug.Log("Currently not muted");
        }
        else
        {
            buttonText.text = "Unmute";
            Debug.Log("Currently muted");
        }
    }

    public void ToggleMute()
    {
        listener.enabled = !listener.enabled;
    }
}
