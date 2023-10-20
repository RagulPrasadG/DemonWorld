using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundService
{
    private SoundServiceScriptableObject soundDataScriptableObject;
    private AudioSource sfxAudioSource;
    private AudioSource bgmAudioSource;

    public SoundService(SoundServiceScriptableObject soundDataScriptableObject, AudioSource sfxAudioSource)
    {
        this.soundDataScriptableObject = soundDataScriptableObject;
        this.sfxAudioSource = sfxAudioSource;
    }
    public void PlaySfx(SoundType soundType)
    {
        AudioClip clip = soundDataScriptableObject.GetSoundClip(soundType);
        if (clip != null)
        {
            sfxAudioSource.clip = clip;
            sfxAudioSource.Play();
        }

    }

}
