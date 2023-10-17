using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerService
{
    public TowerDataScriptableObject towerDataScriptableObject;
    private ProjectileDataScriptableObject projectileDataScriptableObject;
    public ProjectilePool projectilePool;

    public TowerService(TowerDataScriptableObject heroDataScriptableObject,ProjectileDataScriptableObject projectileDataScriptableObject)
    {
        this.towerDataScriptableObject = heroDataScriptableObject;
        this.projectileDataScriptableObject = projectileDataScriptableObject;
        this.projectilePool = new ProjectilePool();
    }
   
    public void CreateTower(Vector3 position)
    {
        TowerController towerController = new TowerController(towerDataScriptableObject.GetTowerData(TowerType.WizardTower),projectileDataScriptableObject,position);
    }

    public void ReturnProjectileToPool(ProjectileController projectileController)
    {
        projectilePool.ReturnItem(projectileController);
    }
}
