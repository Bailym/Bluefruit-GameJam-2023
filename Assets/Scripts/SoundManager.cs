using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource pourAudioSource;
    private float pourSoundStartTime;
    private int pourSoundDurationSeconds = 3;
    
    public AudioSource  DingAudioSource;
    private float dingSoundStartTime;
    private int dingSoundDurationSeconds = 1;

    void Start()
    {
        ResetAllSoundDurationTimers();
    }

    void ResetAllSoundDurationTimers()
    {
        ResetPourSoundTimer();
        ResetDingSoundTimer();
    }

    public void ResetPourSoundTimer()
    {
        pourSoundStartTime = Time.time - pourSoundDurationSeconds;
    }

    public void PlayPourSound()
    {
        if (Time.time > pourSoundStartTime + pourSoundDurationSeconds)
        {
            pourSoundStartTime = Time.time; 
            pourAudioSource.Play();
        }
    }

    public void StopPourSound()
    {
        pourAudioSource.Stop();
    }

    public void ResetDingSoundTimer()
    {
        dingSoundStartTime = Time.time - dingSoundDurationSeconds;
    }

    public void PlayDingSound()
    {
        if (Time.time > dingSoundStartTime + dingSoundDurationSeconds)
        {
            dingSoundStartTime = Time.time;
            DingAudioSource.Play();
        }
    }

    

    
}
