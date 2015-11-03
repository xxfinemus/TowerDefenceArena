using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonMute : MonoBehaviour
{
    [SerializeField]
    private Button myButton;

    public void ToggleMute()
    {
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
            myButton.image.sprite = myButton.spriteState.highlightedSprite;
        }
        else
        {
            AudioListener.volume = 0;
            myButton.image.sprite = myButton.spriteState.pressedSprite;
        }
    }
}
