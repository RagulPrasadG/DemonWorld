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
    private EventServiceScriptableObject eventServiceScriptableObject;
    private EnemyPool enemyPool;
    private WaveData currentWaveData;
    private bool canSpawnEnemies;
    private List<EnemyController> spawnedEnemies = new List<EnemyController>();
    private int totalEnemyCount;


    public EnemyPool GetEnemyPool() => enemyPool;
    public void ReturnEnemyToPool(EnemyController enemyController)
    {
        enemyController.enemyView.transform.rotation = Quaternion.identity;
        enemyController.enemyView.transform.position = GameService.Instance.GetLevelService().GetStartPosition();
        enemyPool.ReturnItem(enemyController);
    }
    public WaveService(List<EnemyDataScriptableObject> enemyData,WaveDataScriptableObject waveDataScriptableObject,
        EventServiceScriptableObject eventServiceScriptableObject)
    {
        this.enemyPool = new EnemyPool(enemyData);
        this.eventServiceScriptableObject = eventServiceScriptableObject;
        this.waveDataScriptableObject = waveDataScriptableObject;
        this.lastWaveId = waveDataScriptableObject.GetWaveLength();
        this.currentWaveId = 0;
        this.canSpawnEnemies = true;
        this.eventServiceScriptableObject.OnEnemyDie.AddListener(CheckForWaveClear);
    }

    public IEnumerator StartWave()
    {
        GameService.Instance.GetSoundService().PlaySfx(SoundType.WaveStart);
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
        EnemyController enemy = enemyPool.GetEnemy(currentWaveData.GetRandomEnemyType());
        enemy.enemyView.gameObject.SetActive(true);
        spawnedEnemies.Add(enemy);
        totalEnemyCount++;

        if (totalEnemyCount >= currentWaveData.amount)
        {
            canSpawnEnemies = false;
            if (currentWaveId < lastWaveId)
            {
                currentWaveId++;
                GameService.Instance.UIService.SetWaveNumber(currentWaveId + 1);
            }
            currentWaveData = waveDataScriptableObject.GetWaveData(currentWaveId);
        }        
           
    }

    public void CheckForWaveClear(EnemyController controller)
    {
        spawnedEnemies.Remove(controller);
        if (spawnedEnemies.Count == 0 && !canSpawnEnemies)
        {
            GameService.Instance.GetSoundService().PlaySfx(SoundType.WaveEnd);
            GameService.Instance.UIService.EnableStartWaveButton();
        }
    }

    public void SetCurrentWaveData()
    {
        this.currentWaveData = waveDataScriptableObject.GetWaveData(currentWaveId);
    }


}
