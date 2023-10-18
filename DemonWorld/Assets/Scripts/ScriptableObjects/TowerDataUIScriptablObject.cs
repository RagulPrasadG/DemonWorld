using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerDataUI",menuName = "Data/NewTowerDataUI")]
public class TowerDataUIScriptablObject : ScriptableObject
{
    [SerializeField] List<TowerDataUI> towerDataUI;
    public List<TowerDataUI> GetTowerData() => towerDataUI;
}

[System.Serializable]
public struct TowerDataUI
{
    public string towerName;
    public Sprite towerSprite;
    public TowerType towerType;
    public float cost;
}