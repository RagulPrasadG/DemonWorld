using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WaveService
{

    private int currentWaveId;
    private int lastWaveId;
    private WaveDataScriptableObject waveDataScriptableObject;
    private List<EnemyDataScriptableObject> enemyDataScriptableObject;
    private EnemyPool enemyPool;
    private WaveData currentWaveData;
    private bool canSpawnEnemies;
    private List<EnemyController> spawnedEnemies = new List<EnemyController>();

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
        this.lastWaveId = waveDataScriptableObject.GetWaveLength();
        this.currentWaveId = 0;
        this.canSpawnEnemies = true;
    }



    public IEnumerator StartWave()
    {
        canSpawnEnemies = true;
        SetCurrentWaveData();
        while (canSpawnEnemies)
        {
            yield return new WaitForSeconds(currentWaveData.spawnRate);
            SpawnEnemy();
            
        }  
    }

    private void SpawnEnemy()
    {
        if (spawnedEnemies.Count >= currentWaveData.amount)
        {
            Debug.Log("Spawning New Wave!!");
            if(currentWaveId < lastWaveId)
            {
                currentWaveId++;
                GameService.Instance.UIService.SetWaveNumber(currentWaveId + 1);
            }
            currentWaveData = waveDataScriptableObject.GetWaveData(currentWaveId);
            GameService.Instance.UIService.EnableStartWaveButton();
            canSpawnEnemies = false;
        }        
        else
        {
            EnemyController enemy = enemyPool.GetEnemy(currentWaveData.GetRandomEnemyType());
            enemy.enemyView.gameObject.SetActive(true);
            spawnedEnemies.Add(enemy);
        }
           
    }

    public void SetCurrentWaveData()
    {
        this.currentWaveData = waveDataScriptableObject.GetWaveData(currentWaveId);
    }


}
