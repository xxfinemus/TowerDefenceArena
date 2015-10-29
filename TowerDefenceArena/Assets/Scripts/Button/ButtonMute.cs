using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonMute : MonoBehaviour
{
    [SerializeField]
    private Camera myCamera;
    private AudioListener listener;

    [SerializeField]
    private Button myButton;

    void Start()
    {
        listener = myCamera.GetComponent<AudioListener>();
    }

    void Update()
    {
        if (listener.enabled)
        {
            myButton.image.sprite = myButton.spriteState.highlightedSprite;
        }
        else
        {
            myButton.image.sprite = myButton.spriteState.pressedSprite;
        }
    }

    public void ToggleMute()
    {
        listener.enabled = !listener.enabled;
    }
}
