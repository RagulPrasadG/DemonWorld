using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demonworld.Utilities;
public class EnemyPool : GenericObjectPool<EnemyController>
{
    private List<EnemyDataScriptableObject> enemyDataScriptableObjects;

    private GameService Gameservice;
    public EnemyPool(List<EnemyDataScriptableObject> enemyDataScriptableObject,GameService gameService)
    {
        this.enemyDataScriptableObjects = enemyDataScriptableObject;
        this.Gameservice = gameService;
    }
    
    public EnemyController GetEnemy(EnemyType enemyType)
    {
        EnemyDataScriptableObject enemyData = enemyDataScriptableObjects.Find(enemy => enemy.enemyType == enemyType);
        EnemyController enemy = GetItem();
        if (enemy.view == null)
        {
            enemy.Init(Gameservice, enemyData);
        }
        return enemy;
    }

    protected override EnemyController CreateItem()
    {
        EnemyController enemyController = new EnemyController();
        return enemyController;
    }


}
