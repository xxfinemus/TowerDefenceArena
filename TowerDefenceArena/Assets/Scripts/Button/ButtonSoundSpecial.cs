﻿// ------------------------------------------------------------------------------------------
// *****This script should only be used if the button can be in a non-interactable state*****
// ------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonSoundSpecial : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip soundClick;
    [SerializeField]
    private AudioClip soundRelease;

    private Button button;
    private bool buttonPressed = false;
    private bool buttonHighlighted = false;

    private void Awake()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    private void Start()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        if (button.interactable)
        {
            if (buttonHighlighted && (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                PlaySoundPress();
                buttonPressed = true;
            }

            if (buttonPressed && (Input.GetMouseButtonUp(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                PlaySoundReleased();
                buttonPressed = false;
            }
        }
    }

    public void EnterButton()
    {
        buttonHighlighted = true;
    }

    public void ExitButton()
    {
        buttonHighlighted = false;
    }

    public void PlaySoundPress()
    {
        audioSource.PlayOneShot(soundClick);
    }

    public void PlaySoundReleased()
    {
        audioSource.PlayOneShot(soundRelease);
    }
}
