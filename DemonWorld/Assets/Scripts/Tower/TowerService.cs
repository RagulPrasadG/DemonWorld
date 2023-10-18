using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demonworld.Services
{
    public class TowerService
    {
        public TowerDataScriptableObject towerDataScriptableObject;
        private ProjectileDataScriptableObject projectileDataScriptableObject;
        private TowerData currentSelectedTower;
        public ProjectilePool projectilePool;

        public TowerService(TowerDataScriptableObject towerDataScriptableObject, ProjectileDataScriptableObject projectileDataScriptableObject)
        {
            this.towerDataScriptableObject = towerDataScriptableObject;
            this.projectileDataScriptableObject = projectileDataScriptableObject;
            this.projectilePool = new ProjectilePool();
        }

        public void SetSelectedTower(TowerType towerType)
        {
            currentSelectedTower = towerDataScriptableObject.GetTowerData(towerType);
        }

        public void CreateTower(Vector3 position)
        {
            TowerController towerController = new TowerController(currentSelectedTower, projectileDataScriptableObject, position);
        }

        public void ReturnProjectileToPool(ProjectileController projectileController)
        {
            projectilePool.ReturnItem(projectileController);
        }
    }
}