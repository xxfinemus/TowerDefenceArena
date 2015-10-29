using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip soundClick;
    [SerializeField]
    private AudioClip soundRelease;

    public void PlaySoundPress()
    {
        audioSource.PlayOneShot(soundClick);
    }

    public void PlaySoundReleased()
    {
        audioSource.PlayOneShot(soundRelease);
    }
}
