using UnityEngine;
using System.Collections;

public class PlayerSounds : MonoBehaviour
{
    private AudioSource source;

    [SerializeField]
    private AudioClip footstep1;
    [SerializeField]
    private AudioClip footstep2;
    [SerializeField]
    private AudioClip footstep3;

    [SerializeField]
    private AudioClip hitMiss;

    // Use this for initialization
    void Start()
    {
        source = transform.parent.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayFootstepSound()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                source.PlayOneShot(footstep1);
                break;
            case 1:
                source.PlayOneShot(footstep2);
                break;
            case 2:
                source.PlayOneShot(footstep3);
                break;

            default:
                break;
        }
    }

    public void PlayHitMissSound()
    {
        source.PlayOneShot(hitMiss);
    }
}
