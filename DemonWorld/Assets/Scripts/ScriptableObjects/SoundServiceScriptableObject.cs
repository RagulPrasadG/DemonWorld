using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundService",menuName = "Services/NewSoundService")]
public class SoundServiceScriptableObject : ScriptableObject
{
    [SerializeField] List<SoundData> soundData;
    
    public AudioClip GetSoundClip(SoundType soundType)
    {
        return soundData.Find(soundData => soundData.soundType == soundType).audioClip;
    }
    public void PlayBGM(SoundType soundType, AudioSource source,bool canLoop = true)
    {
        source.loop = canLoop;
        AudioClip clip = soundData.Find(soundData => soundData.soundType == soundType).audioClip;
        if(clip!=null)
        {
            source.clip = clip;
            source.Play();
        }
    }


}

[System.Serializable]
public struct SoundData
{
    public SoundType soundType;
    public AudioClip audioClip;
}
public enum SoundType
{
    WizardFire,
    EnemySpawn,
    CrossBowFire,
    EnemyDie,
    WaveStart,
    WaveEnd,
    BattleMusic,
    ButtonClick
}
