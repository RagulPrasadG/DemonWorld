using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectionUI : MonoBehaviour
{
    [SerializeField] RectTransform towerViewUIContainer;
    [SerializeField] TowerViewUI towerViewPrefab;
    [SerializeField] TowerDataUIScriptablObject towerDataUIScriptablObject;
    [SerializeField] EventServiceScriptableObject eventServiceScriptableObject;

    private List<TowerViewUI> towerViewUIList = new List<TowerViewUI>();
    private void OnEnable()
    {
        eventServiceScriptableObject.OnTowerSelected.AddListener(OnTowerSelected);  
    }

    private void OnDisable()
    {
        eventServiceScriptableObject.OnTowerSelected.RemoveListener(OnTowerSelected);
    }

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
            towerViewUIList.Add(towerViewUI);
        }
    }

    private void OnTowerSelected(TowerType towerType)
    {
        foreach (TowerViewUI towerDataUI in towerViewUIList)
        {
            towerDataUI.Deselect();
        }
    }

}
