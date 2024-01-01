using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demonworld.Services
{
    public class TowerService
    {
        public TowerDataScriptableObject towerDataScriptableObject;
        public ProjectilePool projectilePool;
        private ProjectileDataScriptableObject projectileDataScriptableObject;
        private TowerData currentSelectedTower;
        private GameService Gameservice;

        public TowerService(GameService Gameservice,
            TowerDataScriptableObject towerDataScriptableObject, 
            ProjectileDataScriptableObject projectileDataScriptableObject)
        {
            this.Gameservice = Gameservice;
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
            TowerController towerController = new TowerController(Gameservice,currentSelectedTower, projectileDataScriptableObject, position);
        }

        public void ReturnProjectileToPool(ProjectileController projectileController)
        {
            projectilePool.ReturnItem(projectileController);
        }
    }
}