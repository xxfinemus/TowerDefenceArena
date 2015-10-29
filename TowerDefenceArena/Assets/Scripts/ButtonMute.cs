using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonMute : MonoBehaviour
{
    [SerializeField]
    private Camera myCamera;
    private AudioListener listener;

    private Image buttonImage;

    [SerializeField]
    private Sprite notMutedSprite;
    [SerializeField]
    private Sprite mutedSprite;

    // Use this for initialization
    void Start()
    {
        listener = myCamera.GetComponent<AudioListener>();
        buttonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (listener.enabled && buttonImage.sprite != notMutedSprite)
        {
            buttonImage.sprite = notMutedSprite;
        }
        else if (!listener.enabled && buttonImage.sprite != mutedSprite)
        {
            buttonImage.sprite = mutedSprite;
        }
    }

    public void ToggleMute()
    {
        listener.enabled = !listener.enabled;
    }
}
