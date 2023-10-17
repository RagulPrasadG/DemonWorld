using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Data/NewPlayerData")]
public class TowerDataScriptableObject : ScriptableObject
{
    [SerializeField] List<TowerData> towersData;
    public TowerData GetTowerData(TowerType towerType)
    {
        return towersData.Find(towerData => towerData.towerType == towerType);
    }

    public Mesh GetPreviewMesh(TowerType towerType)
    {
        foreach(TowerData towerData in towersData)
        {
            if (towerData.towerType == towerType)
                return towerData.previewMesh;
        }
        return null;
    }

}

[System.Serializable]
public class TowerData
{
    public TowerView heroView;
    public TowerType towerType;
    public float attackRate;
    public float rotationSpeed;
    public int attackRange;
    public Mesh previewMesh;
}

public enum TowerType
{
    WizardTower,
    CrossBowTower
}