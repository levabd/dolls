using UnityEngine;
using System.Collections;

public class AnimationSoundsPlayer : MonoBehaviour
{
    public AudioSource toneSource;
    public AudioClip[] toneAudio;
    public Transform ManometersArrow;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void FirstTone()
    {
        AudioClip clip = null;
        float vol = 1.0f;
        clip = toneAudio[0];
        if (clip != null)
        {
            toneSource.PlayOneShot(clip, vol);
        }
    }
    void SecondTone()
    {
        AudioClip clip = null;
        float vol = 1.0f;
        clip = toneAudio[1];
        if (clip != null)
        {
            toneSource.PlayOneShot(clip, vol);
        }
    }
    void ThirdTone()
    {
        AudioClip clip = null;
        float vol = 1.0f;
        clip = toneAudio[2];
        if (clip != null)
        {
            toneSource.PlayOneShot(clip, vol);
        }
    }
    void FourthTone()
    {
        AudioClip clip = null;
        float vol = 1.0f;
        clip = toneAudio[3];
        if (clip != null)
        {
            toneSource.PlayOneShot(clip, vol);
        }
    }
    
}
