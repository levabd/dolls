using UnityEngine;
using System.Collections;

public class AnimationSoundsPlayer : MonoBehaviour
{
    [Header("Источник звука")]
    public AudioSource toneSource;
    [Header("Набор звуков")]
    public AudioClip[] toneAudio;
    [Header("Стрелка манометра")]
    public Transform ManometersArrow;
    private AudioClip clip = null;
    [Header("Громкость звука")]
    public float vol = 1.0f;

    void ToneControl(int tone)
    {
        switch (tone)
        {
            case 1:                
                clip = toneAudio[0];
                if (clip != null)
                {
                    toneSource.PlayOneShot(clip, vol);
                }
                break;
            case 2:
                clip = toneAudio[1];
                if (clip != null)
                {
                    toneSource.PlayOneShot(clip, vol);
                }
                break;
            case 3:
                clip = toneAudio[2];
                if (clip != null)
                {
                    toneSource.PlayOneShot(clip, vol);
                }
                break;
            case 4:
                clip = toneAudio[3];
                if (clip != null)
                {
                    toneSource.PlayOneShot(clip, vol);
                }
                break;

            default:
                break;
        }
    }    
}
