using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectHandler : MonoBehaviour
{
    public AudioClip boing1;
    public AudioClip boing2;
    public AudioClip boing3;
    public AudioClip boing4;
    public AudioClip oof_low1;
    public AudioClip oof_low2;
    public AudioClip oof_low3;
    public AudioClip oof_high1;
    public AudioClip oof_high2;
    public AudioClip oof_high3;
    public AudioSource AudioSource;
    
    void Start()
    {
    }
    
    public void playSound(string sound)
    {
        if (sound == "jump")
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    AudioSource.PlayOneShot(boing1, 2);
                    break;
                case 1:
                    AudioSource.PlayOneShot(boing2, 2);
                    break;
                case 2:
                    AudioSource.PlayOneShot(boing3, 3);
                    break;
                case 3:
                    AudioSource.PlayOneShot(boing4, 2);
                    break;
            }
        }
        if (sound == "death")
        {
            if (PlayerPrefs.GetInt("VoiceTone") == 0)
            {
                switch (Random.Range(0, 3))
                {
                    case 0:
                        AudioSource.PlayOneShot(oof_high1, 2);
                        break;
                    case 1:
                        AudioSource.PlayOneShot(oof_high2, 2.5f);
                        break;
                    case 2:
                        AudioSource.PlayOneShot(oof_high3, 2.5f);
                        break;
                }
            }
            else
            {
                switch (Random.Range(0, 3))
                {
                    case 0:
                        AudioSource.PlayOneShot(oof_low1, 2);
                        break;
                    case 1:
                        AudioSource.PlayOneShot(oof_low2, 2);
                        break;
                    case 2:
                        AudioSource.PlayOneShot(oof_low3, 2);
                        break;
                }
            }
        }
    }
}
