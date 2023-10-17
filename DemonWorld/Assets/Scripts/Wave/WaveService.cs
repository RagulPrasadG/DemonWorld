using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WaveService
{

    private int currentWaveId;
    private int lastWaveId;
    private WaveDataScriptableObject waveDataScriptableObject;
    private EnemyPool enemyPool;
    private WaveData currentWaveData;
    private bool canSpawnEnemies;
    public EnemyPool GetEnemyPool() => enemyPool;
    public void ReturnEnemyToPool(EnemyController enemyController)
    {
        enemyController.enemyView.transform.rotation = Quaternion.identity;
        enemyController.enemyView.transform.position = GameService.Instance.GetLevelService().GetStartPosition();
        enemyPool.ReturnItem(enemyController);
    }
    public WaveService(List<EnemyDataScriptableObject> enemyData,WaveDataScriptableObject waveDataScriptableObject)
    {
        this.enemyPool = new EnemyPool(enemyData);
        this.waveDataScriptableObject = waveDataScriptableObject;
        this.lastWaveId = waveDataScriptableObject.GetWaveLength() - 1;
        this.currentWaveId = 0;
        this.canSpawnEnemies = true;
    }



    public IEnumerator StartWave()
    {
        SetCurrentWaveData();
        while (canSpawnEnemies)
        {
            yield return new WaitForSeconds(currentWaveData.spawnRate);
            SpawnEnemy();
            
        }  
    }

    private void SpawnEnemy()
    {
        EnemyController enemy = enemyPool.GetEnemy(currentWaveData.GetRandomEnemyType());
        enemy.enemyView.gameObject.SetActive(true);
    }

    public void SetCurrentWaveData()
    {
        this.currentWaveData = waveDataScriptableObject.GetWaveData(currentWaveId);
    }


}
