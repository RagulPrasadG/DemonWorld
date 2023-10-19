
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveData",menuName = "Data/NewWaveData")]
public class WaveDataScriptableObject : ScriptableObject
{
    [SerializeField] List<WaveData> waveData;
    public WaveData GetWaveData(int index) => waveData[index];
    public int GetWaveLength() => waveData.Count - 1;

}

[System.Serializable]
public class WaveData
{
    public List<EnemyType> enemyTypes;
    public int amount;
    public int spawnRate;
    public EnemyType GetRandomEnemyType() =>  enemyTypes[Random.Range(0, enemyTypes.Count)];

}

