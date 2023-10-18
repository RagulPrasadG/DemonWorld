using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerDataUI",menuName = "Data/NewTowerDataUI")]
public class TowerDataUIScriptablObject : ScriptableObject
{
    [SerializeField] List<TowerDataUI> towerDataUI;
}

[System.Serializable]
public struct TowerDataUI
{
    public Sprite towerSprite;
    public TowerType towerType;
    public float cost;
}