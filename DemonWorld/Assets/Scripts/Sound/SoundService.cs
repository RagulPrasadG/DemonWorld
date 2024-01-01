using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundService
{
    private SoundServiceScriptableObject soundDataScriptableObject;
    private AudioSource sfxAudioSource;
    private AudioSource bgmAudioSource;

    public SoundService(SoundServiceScriptableObject soundDataScriptableObject, AudioSource sfxAudioSource,
        AudioSource bgmAudioSource)
    {
        this.soundDataScriptableObject = soundDataScriptableObject;
        this.sfxAudioSource = sfxAudioSource;
        this.bgmAudioSource = bgmAudioSource;
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

    public void PlayBGM(SoundType soundType,bool loop)
    {
        AudioClip clip = soundDataScriptableObject.GetSoundClip(soundType);
        bgmAudioSource.loop = true;
        if (clip != null)
        {
            bgmAudioSource.clip = clip;
            bgmAudioSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmAudioSource.Stop();
        bgmAudioSource.loop = false;
    }

    public void PlaySfxAt(SoundType soundType,AudioSource source)
    {
        AudioClip clip = soundDataScriptableObject.GetSoundClip(soundType);
        if (clip != null)
        {
            source.clip = clip;
            source.Play();
        }

    }

}
