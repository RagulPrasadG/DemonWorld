using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demonworld.Utilities;
public class EnemyPool : GenericObjectPool<EnemyController>
{
    private List<EnemyDataScriptableObject> enemyDataScriptableObjects;
    public EnemyPool(List<EnemyDataScriptableObject> enemyDataScriptableObject)
    {
        this.enemyDataScriptableObjects = enemyDataScriptableObject;
    }
    
    public EnemyController GetEnemy(EnemyType enemyType)
    {
        EnemyDataScriptableObject enemyData = enemyDataScriptableObjects.Find(enemy => enemy.enemyType == enemyType);
        EnemyController enemy = GetItem();
        if (enemy.enemyView == null)
        {
            enemy.Init(enemyData);
        }
        return enemy;
    }

    protected override EnemyController CreateItem()
    {
        EnemyController enemyController = new EnemyController();
        return enemyController;
    }


}
