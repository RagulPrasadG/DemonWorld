using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demonworld.Utilities;

public class ProjectilePool : GenericObjectPool<ProjectileController>
{
    private ProjectileData projectileData;

    public ProjectileController GetProjectile(ProjectileData projectileData)
    {
        this.projectileData = projectileData; 
        return GetItem();
    }

    public override ProjectileController GetItem()
    {
        if (pooledItems.Count > 0)
        {
            foreach(PooledItem<ProjectileController> pooledItem in pooledItems)
            {
                if (pooledItem.Item.projectileData.towerType == this.projectileData.towerType
                    && !pooledItem.isUsed
                    )
                {
                    pooledItem.isUsed = true;
                    return pooledItem.Item;
                }
            }
        }
        return CreateNewPooledItem();
    }

    protected override ProjectileController CreateItem()
    {
        ProjectileController projectileController = new ProjectileController(projectileData);
        return projectileController;
    }

    
}
