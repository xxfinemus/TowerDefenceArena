using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonSounds : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip soundClick;
    [SerializeField]
    private AudioClip soundRelease;

    private void Awake()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
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
