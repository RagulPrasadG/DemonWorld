using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileData" , menuName = "Data/NewProjectileData")]
public class ProjectileDataScriptableObject : ScriptableObject
{
    public List<ProjectileData> projectileData;
    public ProjectileData GetProjectileData(TowerType projectileType) => projectileData.Find(projectile => projectile.towerType == projectileType);
}

[System.Serializable]
public struct ProjectileData
{
    public TowerType towerType;
    public float speed;
    public float damageAmount;
    public ProjectileView projectileView;
}


