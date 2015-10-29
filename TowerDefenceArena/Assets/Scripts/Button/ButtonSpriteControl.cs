using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonSpriteControl : MonoBehaviour
{
    Image myImage;
    Button myButton;

    // Use this for initialization
    void Start()
    {
        myImage = GetComponent<Image>();
        myButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myButton.interactable == true)
        {
            myImage.color = Color.white;
        }
        else
        {
            myImage.color = Color.gray;
        }
    }

    public void ToggleInteractive()
    {
        myButton.interactable = !myButton.interactable;
    }
}
