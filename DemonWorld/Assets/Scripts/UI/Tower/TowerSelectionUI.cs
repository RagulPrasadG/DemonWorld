using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectionUI : MonoBehaviour
{
    [SerializeField] RectTransform towerViewUIContainer;
    [SerializeField] TowerViewUI towerViewPrefab;
    [SerializeField] TowerDataUIScriptablObject towerDataUIScriptablObject;

    private void Start()
    {
        CreateTowerSelectionUI();
    }

    public void CreateTowerSelectionUI()
    {
        foreach(TowerDataUI towerDataUI in towerDataUIScriptablObject.GetTowerData())
        {
            TowerViewUI towerViewUI = Instantiate(towerViewPrefab);
            towerViewUI.Init(towerDataUI);
            towerViewUI.transform.SetParent(towerViewUIContainer);
        }
    }

}
