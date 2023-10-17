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

    protected override ProjectileController CreateItem()
    {
        ProjectileController projectileController = new ProjectileController(projectileData);
        return projectileController;
    }

    
}
